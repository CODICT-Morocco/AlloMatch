using AlloMatch.Configurations;
using AlloMatch.DTOs;
using AlloMatch.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlloMatch.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtConfiguration _jwtConfiguration;

        public UserService(UserManager<ApplicationUser> userManager, IOptions<JwtConfiguration> jwtOptions)
        {
            _userManager = userManager;
            _jwtConfiguration = jwtOptions.Value;
        }

        public async Task<AccessTokenDto> Login(string email, string password)
        {
            var normalizedEmail = email.ToUpperInvariant();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
            if (user == null)
                return null;

            if (!await _userManager.CheckPasswordAsync(user, password))
                return null;

            var jwtSecurityToken = await GenerateJWToken(user);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return new AccessTokenDto(token, jwtSecurityToken.Payload.Exp!.Value);
        }

        public async Task<Response> Register(RegisterRequestDto dto)
        {
            var normalizedEmail = dto.Email.ToUpperInvariant();
            if (await _userManager.Users.AnyAsync(u => u.Email == normalizedEmail))
                return new Response(
                    message: "Failed to create account",
                    errors: new List<string>
                    {
                        "Email Already Exists"
                    });


        }

        public async Task<UserProfileDto> GetUserInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return new UserProfileDto(user.Id, user.FirstName, user.LastName, user.Email, roles);
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtConfiguration.DurationInMinutes),
                signingCredentials: signingCredentials);
        }
    }
}

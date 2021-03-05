using AlloMatch.Configurations;
using AlloMatch.Constants;
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

        public async Task<Response<ApplicationUser>> RegisterProfessional(RegisterProfessionalDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return Response<ApplicationUser>.Failure("Used.Email");

            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return Response<ApplicationUser>.Failure("Some.Error");

            result = await _userManager.AddToRoleAsync(user, Roles.Professional);
            if (!result.Succeeded)
                return Response<ApplicationUser>.Failure("Some.Error");

            user.Organisations = new List<Organisation>
            {
                new Organisation
                {
                    Name = dto.OrganisationName,
                    PhoneNumber = dto.PhoneNumber
                }
            };
            await _userManager.UpdateAsync(user);
            return Response<ApplicationUser>.Success(user);
        }

        public async Task<Response<ApplicationUser>> UpdateUserInfos(string userId, UpdateUerInfosDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
                return Response<ApplicationUser>.Failure("User does not exists");
           

            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return Response<ApplicationUser>.Failure("Some.Error");

            return Response<ApplicationUser>.Success(user);


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

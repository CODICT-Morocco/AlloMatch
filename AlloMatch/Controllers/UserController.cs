using AlloMatch.DTOs;
using AlloMatch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AlloMatch.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpPost("login")]
        public async Task<Response<AccessTokenDto>> Login(LoginRequestDto dto)
        {
            var result = await _userService.Login(dto.Email, dto.Password);
            if (result == null)
                return Response<AccessTokenDto>.Failure("User.Not.Found");

            return Response<AccessTokenDto>.Success(result);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<Response<UserProfileDto>> Profile()
            => Response<UserProfileDto>.Success(await _userService.GetUserInfo(_currentUserService.UserId));
    }

    public record LoginRequestDto([Required][EmailAddress] string Email, [Required] string Password);
}

using AlloMatch.DTOs;
using AlloMatch.Services;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<Response<AccessTokenDto>> Login(LoginRequestDto dto)
        {
            var result = await _userService.Login(dto.Email, dto.Password);
            if (result == null)
                return Response<AccessTokenDto>.Failure("User.Not.Found");

            return Response<AccessTokenDto>.Success(result);
        }
    }

    public record LoginRequestDto([Required][EmailAddress] string Email, [Required] string Password);
}

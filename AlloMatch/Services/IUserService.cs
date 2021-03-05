using AlloMatch.DTOs;
using AlloMatch.Entities;
using System.Threading.Tasks;

namespace AlloMatch.Services
{
    public interface IUserService
    {
        Task<AccessTokenDto> Login(string email, string password);
        Task<UserProfileDto> GetUserInfo(string userId);
        Task<Response<ApplicationUser>> RegisterProfessional(RegisterProfessionalDto dto);
        Task<Response<ApplicationUser>> UpdateUserInfos(string userId, UpdateUerInfosDto dto);
    }
}

using AlloMatch.DTOs;
using System.Threading.Tasks;

namespace AlloMatch.Services
{
    public interface IUserService
    {
        Task<AccessTokenDto> Login(string email, string password);
        Task<UserProfileDto> GetUserInfo(string userId);
    }
}

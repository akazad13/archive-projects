using WMC.Models;
using WMC.Models.Dto;

namespace WMC.Services
{
    public interface IAccountService
    {
        Task<bool> Register(UserForRegisterDto userForRegisterDto, string role);
        Task<bool> Login(string username, string password);
        Task<User> GetUser(int userid);
    }
}
using TakeItToTheCloud.Models;
using TakeItToTheCloud.Models.Dto;

namespace TakeItToTheCloud.Services
{
    public interface IAccountService
    {
        Task<bool> Register(UserForRegisterDto userForRegisterDto, string role);
        Task<bool> Login(string username, string password);
        Task<User> GetUser(int userid);
    }
}
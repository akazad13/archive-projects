using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TakeItToTheCloud.Models;
using TakeItToTheCloud.Models.Dto;

namespace TakeItToTheCloud.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public AccountService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> Register(UserForRegisterDto userForRegisterDto, string role)
        {

            var currentUser = await _userManager.FindByEmailAsync(userForRegisterDto.Email);

            if (currentUser != null)
            {
                throw new Exception("There is an account with the same username!");
            }

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userToCreate.UserName);
                await _userManager.AddToRolesAsync(user, new[] { role });
                return true;
            }
            throw new Exception(result.Errors.ToString());
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("There is no account exist!");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (result.Succeeded)
            {
                return true;
            }

            throw new Exception("Please enter valid credentials!");
        }

        public async Task<User> GetUser(int userid)
        {
            return await _userManager.FindByIdAsync(userid.ToString());
        }
    }
}

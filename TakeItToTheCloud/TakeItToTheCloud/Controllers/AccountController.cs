using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TakeItToTheCloud.Models;
using TakeItToTheCloud.Models.Dto;
using TakeItToTheCloud.Services;

namespace TakeItToTheCloud.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("_Login");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View("_Register");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {

            try
            {
                var p = await _accountService.Register(userForRegisterDto, "User");
                if (p == true)
                {
                    return Ok(new { success = true, message = "Registration Successful" });
                }
                else
                {
                    return Ok(new { success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var p = await _accountService.Login(email, password);
                if (p == true)
                {
                    return Ok(new { redirectRoute = "dashboard" });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> GetProfile()
        {
            int.TryParse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out int userid);
            var model = await _accountService.GetUser(userid);
            return PartialView("_AboutMe", model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }
    }
}

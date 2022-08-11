using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Controllers
{

    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        private RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<IdentityUser> userMgr,
                               SignInManager<IdentityUser> signInMgr,
                               RoleManager<IdentityRole> roleMgr
                               )
        {
            userManager = userMgr;
            signInManager = signInMgr;
            roleManager = roleMgr;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel creds,
                string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (await DoLogin(creds))
                {
                    return Redirect(returnUrl ?? "/");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }
            return View(creds);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string redirectUrl)
        {
            await signInManager.SignOutAsync();
            return Redirect(redirectUrl ?? "/");
        }

        private async Task<bool> DoLogin(LoginViewModel creds)
        {
            IdentityUser user = await userManager.FindByNameAsync(creds.Name);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(user, creds.Password,
                        false, false);
                return result.Succeeded;
            }
            return false;
        }

        [HttpPost("/api/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel creds)
        {
            if (ModelState.IsValid && await DoLogin(creds))
            {
                IdentityUser user = await userManager.FindByNameAsync(creds.Name);
                bool isAdmin = await userManager.IsInRoleAsync(user, "Administrator");
                if (isAdmin)
                {
                    return Ok(new { role = "admin" });
                }
                else
                {
                    return Ok(new { role = "user" });
                }

            }
            return BadRequest();
        }


        private async Task<bool> DoSignUp(SignupViewModel creds)
        {
            IdentityUser user = await userManager.FindByNameAsync(creds.Name);
            if (user == null)
            {

                IdentityRole role = await roleManager.FindByNameAsync("User");

                if (role == null)
                {
                    role = new IdentityRole("User");
                    IdentityResult result = await roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Cannot create role: "
                            + result.Errors.FirstOrDefault());
                    }
                }

                if (user == null)
                {
                    user = new IdentityUser(creds.Name);
                    IdentityResult result
                        = await userManager.CreateAsync(user, creds.Password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Cannot create user: "
                            + result.Errors.FirstOrDefault());
                    }
                }

                if (!await userManager.IsInRoleAsync(user, "User"))
                {
                    IdentityResult result
                        = await userManager.AddToRoleAsync(user, "User");
                    if (!result.Succeeded)
                    {
                        throw new Exception("Cannot add user to role: "
                            + result.Errors.FirstOrDefault());
                    }
                }

                return true;
            }
            return false;
        }

        [HttpPost("/api/account/signup")]
        public async Task<IActionResult> SignUp([FromBody] SignupViewModel creds)
        {
            if (ModelState.IsValid && await DoSignUp(creds))
            {
                return Ok("true");
            }
            return BadRequest();
        }

        [HttpPost("/api/account/logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }
    }

    public class LoginViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }



    public class SignupViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

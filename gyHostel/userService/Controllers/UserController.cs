using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using userService.DTO;
using userService.Security;

namespace userService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserController(IConfiguration config, IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = config;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userRepo.Get());
        }

        // GET api/<controller>/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userRepo.Get(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            user.Password = new HashingManager().HashToString(user.Password);

            _userRepo.Add(user);
            if (_userRepo.SaveAll())
            {
                return Created("", user);
            }
            return BadRequest($"failed to add new user");
            
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userRepo.Get(id);

            if (user == null)
                return NotFound();

            _mapper.Map(dto, User); // (from, to)
            
            if ( _userRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Updating user {id} failed on save");
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userRepo.Get(id);

            if (user == null)
                return NoContent();

            _userRepo.Delete(user);
            if (_userRepo.SaveAll())
            {
                return Ok();
            }
            return BadRequest($"Deleting user {id} failed on save");
        }


        // POST api/<controller>/login
        [HttpPost("login")]
        public IActionResult Login(UserForLoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = _userRepo.GetUserByAccount(dto.Account);

            if (user == null)
                return Unauthorized();

            if(new HashingManager().VerifyPassword(dto.Password, user.Password))
            {
                return Ok(new
                {
                    token = GenerateJwtToken(user)
                });
            }
            return Unauthorized();
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.User_Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            // After this, inject the IConfiguration class, then in appsetting add Token
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes("abcdefghijklmnbvcxzssdffggsrg"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

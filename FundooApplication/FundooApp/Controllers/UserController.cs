using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace FundooApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly string _secret;
        private readonly string _issuer;
        public UserController(IUserBL dataRepository, IConfiguration config)
        {
            _userBL = dataRepository;
            _secret = config.GetSection("Jwt").GetSection("Key").Value;
            _issuer = config.GetSection("Jwt").GetSection("Issuer").Value;
        }

        // GET: api/Employee
        /// <summary>    
        /// ValuesController Api Get method    
        /// </summary>    
        /// <returns></returns>  
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UserModel> users = _userBL.GetAll();
            return Ok(users);
        }

        // POST: api/User
        [HttpPost]
        [AllowAnonymous]
        public IActionResult UserRegister([FromBody] UserModel user)
        {
            if (user == null)
            {
                return BadRequest("Employee is null.");
            }

            bool result = _userBL.UserRegister(user);
            if (result == true)
            {
                return this.Ok(new { success = true, message = "User Registration is successful" });
            }
            else 
            {
                return this.BadRequest(new { success = false, message ="User Registration failed"});
            }

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] LoginRequestModel model)
        {
            var user = _userBL.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Audience = _issuer,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", Convert.ToString(user.UserId)),
                    new Claim(ClaimTypes.Email, model.Email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1440),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                //Gender = user.Gender,
                Email = user.Email,
                Token = tokenString
            });
        }


    }
}

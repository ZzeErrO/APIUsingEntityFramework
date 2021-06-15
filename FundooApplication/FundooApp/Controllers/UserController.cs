using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Cors;

namespace FundooApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        //private readonly INotesBL _noteBL;
        private readonly string _secret;
        private readonly string _issuer;
        public UserController(IUserBL dataRepository, IConfiguration config/*, INotesBL noteBL*/)
        {
            _userBL = dataRepository;
            //_noteBL = noteBL;
            _secret = config.GetSection("Jwt").GetSection("Key").Value;
            _issuer = config.GetSection("Jwt").GetSection("Issuer").Value;
        }

        // GET: api/Employee
        /// <summary>    
        /// ValuesController Api Get method    
        /// </summary>    
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<UserModel> users = _userBL.GetAll();
            return Ok(users);
        }

        // POST: api/User
        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult UserRegister([FromBody] UserRegistration user)
        {
            if (user == null)
            {
                return BadRequest("Employee is null.");
            }

            foreach (var item in _userBL.GetAll())
            {
                if (item.Email == user.Email)
                {
                    return this.Ok(new { success = false, message = new FundooCustomExceptions(FundooCustomExceptions.ExceptionType.USER_EXIST, " Email Already Exist ") });
                }
            }
            try
            {
                ValidationContext context = new ValidationContext(user, null, null);
                List<ValidationResult> results = new List<ValidationResult>();
                bool valid = Validator.TryValidateObject(user, context, results, true);
                if (valid)
                {
                    bool result = _userBL.UserRegister(user);
                    if (result == true)
                    {
                        return this.Ok(new { success = true, message = "User Registration is successful" });
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = "User Registration failed" });
                    }
                }
                return this.Ok(new { success = true, message = results[0].MemberNames.First() + "\n" + results[0].ErrorMessage });
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message});
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] LoginRequestModel model)
        {
            try
            {
                var user = _userBL.Authenticate(model.Email, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Password is incorrect" });

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
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }

        }
        
        [AllowAnonymous]
        [HttpPut]
        [Route("ForgetPassword")]
        public IActionResult ForgotPassword(string emailAddress)
        {
            try
            {
                var result = this._userBL.ForgetPassword(emailAddress);
                if (result.Equals(true))
                {
                    return this.Ok(new { Status = true, Message = "Mail Sent Sucessfully", Data = emailAddress });
                }
                return this.BadRequest(new { Status = false, Message = "Email is not correct:Please enter valid email" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("ResetPassword")]
        ////Post: /api/User/ResetPassword
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (resetPasswordModel.Password != resetPasswordModel.ConfirmPassword)
                {
                    return this.BadRequest(new { success = false, message = "Password does not match confirm Password" });
                }
                var result = _userBL.ResetPassword(resetPasswordModel.Email, resetPasswordModel.Password);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Password Reset Successfully" });
                }
                else
                {
                    return this.Ok(new { success = false, message = "Password Reset Failed Check Email Or Password" });
                }
            }
            catch(Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }

            // DELETE: api/Employee/5
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            
            UserModel user = _userBL.Get(id);
            if (user == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            _userBL.Delete(user);
            return NoContent();
        }


    }
}

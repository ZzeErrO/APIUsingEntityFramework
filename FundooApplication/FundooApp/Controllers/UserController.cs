using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Models;

namespace FundooApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        public UserController(IUserBL dataRepository)
        {
            _userBL = dataRepository;
           // Task task = new Task(RegisterAsync);
        }

        // GET: api/Employee
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = _userBL.GetAll();
            return Ok(users);
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            User user = _userBL.Get(id);

            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult UserRegister([FromBody] User user)
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

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            User userToUpdate = _userBL.Get(id);
            if (userToUpdate == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            bool result = _userBL.Update(userToUpdate, user);
            if (result == true)
            {
                return this.Ok(new { success = true, message = "Successfully edited" });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Editing Failed" });
            }
           
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            User user = _userBL.Get(id);
            if (user == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            _userBL.Delete(user);
            return NoContent();
        }

        /*
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(User user)
        {
            try
            {
                if (user != null)
                {
                    decimal id = await _userBL.RegisterAsync(user);
                    if (id != 0)
                    {
                        return Ok(new { success = true, message = "User is Registered Successfully" });
                    }
                    else
                    {
                        return Ok(new { success = false, message = "User is not Registered" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Insufficient details..." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            finally 
            { 
            
            }
        }

        [Route("UserLogin")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    AccountLoginResponce Data = _userBL.Login(model);
                    if (Data.Token != null)
                    {
                        return Ok(new { success = true, message = "Login successful!", Data });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Wrong Email or Password" });
                    }
                }
                else
                {
                    return BadRequest(new { success =false, message = "Invalid credentials" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message});
            }
        }*/

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Models;

namespace FundooApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //IUserBL userBL;
        private readonly IUserBL _dataRepository;
        public UserController(/*IUserBL userBL, */IUserBL dataRepository)
        {
            //this.userBL = userBL;
            _dataRepository = dataRepository;
        }

       /* [HttpPost]
        public IActionResult Sampleapi(Model model)
        {
            Model Result = this.userBL.SampleApi(model);
            return this.Ok(new { success = true, data = Result });
        }*/

        // GET: api/Employee
       /* [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<User> users = _dataRepository.GetAll();
            return Ok(users);
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            User user = _dataRepository.Get(id);

            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            return Ok(user);
        }
       */
        // POST: api/Employee
        [HttpPost]
        public IActionResult UserRegister([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Employee is null.");
            }

           bool result = _dataRepository.UserRegister(user);
            if (result == true)
            {
                return this.Ok(new { success = true, message = "User Registration is successful" });
            }
            else {
                return this.BadRequest(new { success = false, message ="User Registration failed"});
            }
        }
        /*
        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null.");
            }

            User userToUpdate = _dataRepository.Get(id);
            if (userToUpdate == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            _dataRepository.Update(userToUpdate, user);
            return NoContent();
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            User user = _dataRepository.Get(id);
            if (user == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            _dataRepository.Delete(user);
            return NoContent();
        }
        */
    }
}

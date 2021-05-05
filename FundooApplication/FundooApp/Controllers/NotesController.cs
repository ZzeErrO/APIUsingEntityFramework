using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INotesBL _notesBL;
        public NotesController(INotesBL dataRepository)
        {
            _notesBL = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Note> note = _notesBL.GetAll();
            return Ok(note);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostNote([FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note is null.");
            }

            bool result = _notesBL.PostNote(note);
            if (result == true)
            {
                return this.Ok(new { success = true, message = "Note created successfully" });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Note Creation failed" });
            }

        }
    }
}

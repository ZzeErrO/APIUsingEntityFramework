using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INotesBL _notesBL;
        public NotesController(INotesBL dataRepository)
        {
            _notesBL = dataRepository;
        }
        [HttpGet("UserId")]
        public string GetTokenType()
        {
            return User.FindFirst("Id").Value;
        }

        [HttpGet("GetAllNotesFromDatabase")]
        public IActionResult GetAll()
        {
            IEnumerable<Note> note = _notesBL.GetAll();
            return Ok(note);
        }

        [HttpGet("GetUserNotes")]
        public IActionResult GetNote()
        {
            IEnumerable<Note> note = _notesBL.GetNote(Convert.ToInt64(GetTokenType()));
            return Ok(note);
        }

        // GET: api/Employee/5
        [HttpGet("RecordFromNoteId")]
        public IActionResult Get(long id)
        {
            Note note = _notesBL.Get(id);

            if (note == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            return Ok(note);
        }

        [HttpPost("AddNewNote")]
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

        // PUT: api/Note/5
        [HttpPut("UpdateNote")]
        public IActionResult Put(long id, [FromBody] Note note)
        {
            if (note == null)
            {
                return BadRequest("Note is null.");
            }

            Note userToUpdate = _notesBL.Get(id);
            if (userToUpdate == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            bool result = _notesBL.Update(userToUpdate, note);
            if (result == true)
            {
                return this.Ok(new { success = true, message = "Successfully edited" });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Editing Failed" });
            }

        }

        [HttpPut("MoveToArchive")]
        public IActionResult MoveToArchive(long id)
        {
            Note userToUpdate = _notesBL.Get(id);
            if (userToUpdate == null)
            {
                return NotFound("The User record couldn't be found.");
            }

            bool result = _notesBL.MoveToArchive(userToUpdate);
            if (result == true)
            {
                return this.Ok(new { success = true, message = "Successfully moved" });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Moving Failed" });
            }

        }


        [HttpPut("DeleteToTrash")]
        public IActionResult DeleteToTrash(long id)
        {
            Note note = _notesBL.Get(id);
            if (note == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            if (note.IsTrash == true)
            {
                return BadRequest("Note is already in Trash");
            }

            IEnumerable<Note> result = _notesBL.DeleteToTrash(note);
            return Ok(result);
        }

        // DELETE: api/Employee/5
        [HttpDelete("DeleteFromTrash")]
        public IActionResult DeleteFromTrash(long id)
        {
            Note note = _notesBL.Get(id);
            if (note == null)
            {
                return NotFound("The User record couldn't be found.");
            }
            if (note.IsTrash == true)
            {
                _notesBL.DeleteFromTrash(note);
                return NoContent();
            }
            else 
            {
                return BadRequest("Note is not in trash");
            }
        }
        
    }
}

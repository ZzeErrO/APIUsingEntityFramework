using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessManager.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class NotesController : Controller
    {
        private readonly INotesBL _notesBL;
        public NotesController(INotesBL dataRepository)
        {
            _notesBL = dataRepository;
        }
        private string GetTokenType()
        {
            return User.FindFirst("Id").Value;
        }

        //[HttpGet("Database")]
        private IActionResult GetAll()
        {
            IEnumerable<Note> note = _notesBL.GetAll();
            return Ok(note);
        }

        [HttpGet]
        public IActionResult GetNote()
        {
            var getId = Convert.ToInt64(GetTokenType());
            IEnumerable<Note> note = _notesBL.GetNote(getId);
            return Ok(note);
        }

        [HttpGet("Reminder")]
        public IActionResult GetReminderNote()
        {
            IEnumerable<Note> note = _notesBL.GetReminderNote(Convert.ToInt64(GetTokenType()));
            return Ok(note);
        }

        [HttpGet("Pin")]
        public IActionResult GetPinnedNote()
        {
            IEnumerable<Note> note = _notesBL.GetPinnedNote(Convert.ToInt64(GetTokenType()));
            return Ok(note);
        }

        [HttpGet("Archive")]
        public IActionResult GetArchiveNote()
        {
            IEnumerable<Note> note = _notesBL.GetArchiveNote(Convert.ToInt64(GetTokenType()));
            return Ok(note);
        }

        [HttpGet("Trash")]
        public IActionResult GetTrashNote()
        {
            IEnumerable<Note> note = _notesBL.GetTrashNote(Convert.ToInt64(GetTokenType()));
            return Ok(note);
        }

        [HttpPost]
        public IActionResult AddNewNote([FromBody] Note note)
        {
            try
            {
                if (note == null)
                {
                    return BadRequest("Note is null.");
                }

                bool result = _notesBL.PostNote(note, Convert.ToInt64(GetTokenType()));
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Note created successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Note Creation failed" });
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }

        }

        // PUT: api/Note/5
        [HttpPut("{id}")]
        public IActionResult UpdateNote(long id, [FromBody] NoteModel note)
        {
            try
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
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }

        }

        [HttpPut("{id}/Image")]
        public IActionResult Image(long id, string image)
        {
            try
            {
                Note userToUpdate = _notesBL.Get(id);
                if (userToUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.Image(userToUpdate, image);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully Added Image" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Image Adding Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}/{color}")]
        public IActionResult Color(long id, string color)
        {
            try
            {
                Note userToUpdate = _notesBL.Get(id);
                if (userToUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.Color(userToUpdate, color);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully Added Color" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Color Adding Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}/Reminder")]
        public IActionResult Reminder(long id, DateTime reminder)
        {
            try
            {
                Note userToUpdate = _notesBL.Get(id);
                if (userToUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.Reminder(userToUpdate, reminder);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully changed Reminder" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed Changing Reminder" });
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}/Pin")]
        public IActionResult ToPin(long id)
        {
            try
            {
                Note userToUpdate = _notesBL.Get(id);
                if (userToUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.ToPin(userToUpdate);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully pined" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed Pinning" });
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{id}/Archive")]
        public IActionResult MoveToArchive(long id)
        {
            try
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
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }

        }

        [HttpPut("{id}/UnArchiveOrUnTrash")]
        public IActionResult UnArchiveOrUnTrash(long id)
        {
            try
            {
                Note userToUpdate = _notesBL.Get(id);
                if (userToUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.UnArchiveOrUnTrash(userToUpdate);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully moved" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Moving Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }

        }

        [HttpDelete("{id}/Trash")]
        public IActionResult DeleteToTrash(long id)
        {
            try
            {
                Note userToUpdate = _notesBL.Get(id);
                if (userToUpdate == null)
                {
                    return NotFound("The User record couldn't be found.");
                }

                bool result = _notesBL.DeleteToTrash(userToUpdate);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Successfully moved" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Moving Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.Ok(new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFromTrash(long id)
        {
            try
            {
                Note note = _notesBL.Get(id);
                if (note == null)
                {
                    return NotFound("The User record couldn't be found.");
                }
                if (note.IsTrash == true)
                {
                    _notesBL.DeleteFromTrash(note);
                    return this.Ok(new { success = true, message = "Note deleted successfully" });
                }
                else
                {
                    return BadRequest("Note is not in trash");
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        
    }
}

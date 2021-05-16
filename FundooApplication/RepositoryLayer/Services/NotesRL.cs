using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        readonly UserContext _noteContext;
        public NotesRL(UserContext context)
        {
            _noteContext = context;
        }
        public IEnumerable<Note> GetAll()
        {
            return _noteContext.Notes.ToList();
        }

        public IEnumerable<Note> GetNote(long Id)
        {
            try
            {
                return _noteContext.Notes.Where(e => e.UserId == Id && e.IsArchive == false && e.IsTrash == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<Note> GetReminderNote(long Id)
        {
            try
            {
                return _noteContext.Notes.Where(e => e.UserId == Id && e.Reminder != null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<Note> GetPinnedNote(long Id)
        {
            try
            {
                return _noteContext.Notes.Where(e => e.UserId == Id && e.IsPin == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<Note> GetArchiveNote(long Id)
        {
            try
            {
                return _noteContext.Notes.Where(e => e.UserId == Id && e.IsArchive == true && e.IsTrash == false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<Note> GetTrashNote(long Id)
        {
            try
            {
                return _noteContext.Notes.Where(e => e.UserId == Id && e.IsTrash == true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Note Get(long id)
        {
            try
            {
                return this._noteContext.Notes.FirstOrDefault(e => e.NoteId == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PostNote(Note note, long Id)
        {
            try
            {

                Note _user = new Note()
                {
                    Title = note.Title,
                    Message = note.Message,
                    Reminder = note.Reminder,
                    Color = note.Color,
                    Image = note.Image,
                    Collaborator = note.Collaborator,
                    IsPin = note.IsPin,
                    IsArchive = note.IsArchive,
                    IsTrash = note.IsTrash,
                    UserId = Id
                };
                _noteContext.Notes.Add(_user);
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(Note note, NoteModel entity)
        {
            try
            {
                note.Title = entity.Title;
                note.Message = entity.Message;
                note.Reminder = entity.Reminder;
                note.Color = entity.Color;
                note.Image = entity.Image;
                note.Collaborator = entity.Collaborator;
                note.IsPin = entity.IsPin;
                note.IsArchive = entity.IsArchive;
                note.IsTrash = entity.IsTrash;

                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Image(Note note, string image)
        {
            try
            {
                if (note.Image == null)
                {
                    note.Image = image;
                    int result = _noteContext.SaveChanges();
                    if (result <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                note.Image = note.Image + "," + image;
                int result1 = _noteContext.SaveChanges();
                if (result1 <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Reminder(Note note, DateTime reminder)
        {
            try
            {
                note.Reminder = reminder;
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ToPin(Note note)
        {
            try
            {
                note.IsPin = true;
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool MoveToArchive(Note note)
        {
            note.IsArchive = true;
            int result = _noteContext.SaveChanges();
            if (result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool DeleteToTrash(Note note)
        {
            try
            {
                note.IsTrash = true;
                int result = _noteContext.SaveChanges();
                if (result <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromTrash(Note note)
        {
            try
            {
                _noteContext.Notes.Remove(note);
                _noteContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

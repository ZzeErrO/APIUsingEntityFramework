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
            return _noteContext.Notes.Where(e => e.UserId == Id && e.IsArchive == false && e.IsTrash == false);
        }

        public Note Get(long id)
        {
            return this._noteContext.Notes.FirstOrDefault(e => e.NoteId == id);
        }
        public bool PostNote(Note note)
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
                UserId = note.UserId
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
        public bool Update(Note note, Note entity)
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
            note.UserId = entity.UserId;
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
        public IEnumerable<Note> DeleteToTrash(Note note)
        {
            note.IsTrash = true;
            return _noteContext.Notes.ToList();
        }

        public void DeleteFromTrash(Note note)
        {
            _noteContext.Notes.Remove(note);
            _noteContext.SaveChanges();
        }
    }
}

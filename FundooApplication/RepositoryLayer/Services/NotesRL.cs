using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Models;

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
    }
}

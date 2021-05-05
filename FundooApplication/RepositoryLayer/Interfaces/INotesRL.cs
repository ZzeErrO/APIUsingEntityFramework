using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {
        public IEnumerable<Note> GetAll();
        bool PostNote(Note note);
    }
}

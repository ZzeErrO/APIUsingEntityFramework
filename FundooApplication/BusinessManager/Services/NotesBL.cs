using System;
using System.Collections.Generic;
using System.Text;
using BusinessManager.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;

namespace BusinessManager.Services
{
    public class NotesBL : INotesBL
    {
        INotesRL noteRL;
        public NotesBL(INotesRL _noteRL)
        {
            this.noteRL = _noteRL;
        }
        public IEnumerable<Note> GetAll()
        {
            return this.noteRL.GetAll();
        }
        public bool PostNote(Note note)
        {
            return this.noteRL.PostNote(note);
        }
    }
}

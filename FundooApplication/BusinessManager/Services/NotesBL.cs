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
        public IEnumerable<Note> GetNote(long Id)
        { 
            return this.noteRL.GetNote(Id);
        }
        public Note Get(long id)
        {
            return this.noteRL.Get(id);
        }
        public bool PostNote(Note note)
        {
            return this.noteRL.PostNote(note);
        }
        public bool Update(Note note, Note entity)
        {
            return this.noteRL.Update(note, entity);
        }
        public bool MoveToArchive(Note note)
        {
            return this.noteRL.MoveToArchive(note);
        }
        public IEnumerable<Note> DeleteToTrash(Note note)
        {
            return this.noteRL.DeleteToTrash(note);
        }
        public void DeleteFromTrash(Note note)
        {
            this.noteRL.DeleteFromTrash(note);
        }
    }
}

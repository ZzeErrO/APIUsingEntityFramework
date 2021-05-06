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
        public IEnumerable<Note> GetNote(long Id);
        public Note Get(long id);
        public bool Update(Note note, Note entity);
        public bool MoveToArchive(Note note);
        public IEnumerable<Note> DeleteToTrash(Note note);
        public void DeleteFromTrash(Note note);
    }
}

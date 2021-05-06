using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessManager.Interfaces
{
    public interface INotesBL
    {
        public IEnumerable<Note> GetAll();
        public IEnumerable<Note> GetNote(long Id);
        public Note Get(long id);
        bool PostNote(Note note);
        public bool Update(Note note, Note entity);
        public bool MoveToArchive(Note note);
        public IEnumerable<Note> DeleteToTrash(Note note);
        public void DeleteFromTrash(Note note);
    }
}

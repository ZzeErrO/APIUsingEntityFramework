using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessManager.Interfaces
{
    public interface INotesBL
    {
        public IEnumerable<Note> GetAll();
        bool PostNote(Note note);
    }
}

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
        public IEnumerable<Note> GetPinnedNote(long Id);
        public IEnumerable<Note> GetReminderNote(long Id);
        public IEnumerable<Note> GetArchiveNote(long Id);
        public IEnumerable<Note> GetTrashNote(long Id);
        public Note Get(long id);
        bool PostNote(Note note, long Id);
        public bool Update(Note note, NoteModel entity);
        public bool Image(Note note, string image);
        public bool Reminder(Note note, DateTime reminder);
        public bool ToPin(Note note);
        public bool MoveToArchive(Note note);
        public bool DeleteToTrash(Note note);
        public void DeleteFromTrash(Note note);
    }
}

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
            try
            {
                return this.noteRL.GetNote(Id);
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
            return this.noteRL.GetReminderNote(Id);
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
                return this.noteRL.GetPinnedNote(Id);
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
                return this.noteRL.GetArchiveNote(Id);
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
                return this.noteRL.GetTrashNote(Id);
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
                return this.noteRL.Get(id);
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
                return this.noteRL.PostNote(note, Id);
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
                return this.noteRL.Update(note, entity);
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
                return this.noteRL.Image(note, image);
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
                return this.noteRL.Reminder(note, reminder);
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
                return this.noteRL.ToPin(note);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool MoveToArchive(Note note)
        {
            try
            {
                return this.noteRL.MoveToArchive(note);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteToTrash(Note note)
        {
            try
            {
                return this.noteRL.DeleteToTrash(note);
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
                this.noteRL.DeleteFromTrash(note);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

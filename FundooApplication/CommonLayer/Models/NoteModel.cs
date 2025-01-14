﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public string Collaborator { get; set; }
        public bool IsPin { get; set; }
        public bool IsArchive { get; set; }
        public bool IsTrash { get; set; }
    }
}

using System;

namespace NotesPCL.Models
{
    public class Note
    {
        public Note(string content, DateTime created)
        {
            Content = content;
            Created = created;
        }

        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
using System;

namespace NotesPCL.Models
{
    public class Note
    {
        public Note()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
    }
}
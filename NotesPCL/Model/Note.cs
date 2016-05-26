using System;
using GalaSoft.MvvmLight;

namespace NotesPCL.Model
{
    public class Note : ViewModelBase
    {
        public Note(String content, DateTime created)
        {
            Content = content;
            Created = created;
        }

        public String Content { get; set; }
        public DateTime Created { get; set; }
    }
}

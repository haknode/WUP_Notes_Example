using System;
using GalaSoft.MvvmLight;

namespace NotesPCL.Models
{
    public class Note : ObservableObject
    {
        public Note()
        {
            Id = Guid.NewGuid();
            LastModified = DateTime.MinValue;
            CreationLocation = null;
        }

        public Guid Id { get; private set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
        public GeoLocation CreationLocation { get; set; }
        public Boolean IsNotEmpty => ! string.IsNullOrWhiteSpace(Content);

        public Note Clone()
        {
            return new Note
            {
                Id = this.Id,
                Content = this.Content,
                LastModified =  this.LastModified,
                CreationLocation = this.CreationLocation,
            };
        }
    }
}
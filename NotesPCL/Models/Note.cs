using System;
using GalaSoft.MvvmLight;

namespace NotesPCL.Models
{
    public class Note : ObservableObject
    {
        private GeoLocation creationLocation;

        public Note()
        {
            LastModified = DateTime.Now;
            CreationLocation = GeoLocation.InvalidGeoLocation;
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }

        public GeoLocation CreationLocation
        {
            get { return creationLocation; }
            set
            {
                if (creationLocation != value)
                {
                    creationLocation = value ?? GeoLocation.InvalidGeoLocation;
                }
            }
        }

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
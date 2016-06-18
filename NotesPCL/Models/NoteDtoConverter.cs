using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesPCL.Models
{
    public class NoteDtoConverter
    {
        public static NoteDto NoteToDto(Note note, String tenantId)
        {
            var noteDto = new NoteDto
            {
                Id = note.Id,
                TenantId = tenantId,
                Content = note.Content,
                CreatedAt = note.LastModified,
            };

            if (note.CreationLocation.IsValid)
            {
                noteDto.Latitude = note.CreationLocation.Latitude;
                noteDto.Longitude = note.CreationLocation.Longitude;
            }
            else
            {   //Use Latitude=0 and Longitude=0 to indicate invalid coordinate, because REST service is not offering other possibility
                noteDto.Latitude = 0.0;
                noteDto.Longitude = 0.0;
            }

            return noteDto;
        }

        public static Note DtoToNote(NoteDto noteDto)
        {
            var note = new Note()
            {
                Id = noteDto.Id,
                Content = noteDto.Content,
                LastModified = noteDto.CreatedAt,
            };

            if (noteDto.Latitude != 0.0 && noteDto.Longitude != 0.0)
            {
                note.CreationLocation = new GeoLocation(noteDto.Latitude, noteDto.Longitude);
            }
            else
            {   //Use Latitude=0 and Longitude=0 to indicate invalid coordinate, because REST service is not offering other possibility
                note.CreationLocation = GeoLocation.InvalidGeoLocation;
            }

            return note;
        }
    }
}

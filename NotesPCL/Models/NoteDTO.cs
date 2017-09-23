using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesPCL.Models
{
    //Note class as it is provided by the REST API (Data Transfer Object)
    //A Note is converted into a NoteDto before sending it to the REST API and vice versa
    public class NoteDto
    {
        public int Id { get; set; }
        public String TenantId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesPCL.Model
{
    public class Settings
    {
        public int NumberOfNotesInListView { get; set; }

        public static Settings DefaultSettings => new Settings()
        {
            NumberOfNotesInListView = 10
        };
    }
}

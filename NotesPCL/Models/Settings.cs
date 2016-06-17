using System;

namespace NotesPCL.Models
{
    public class Settings
    {
        public int NumberOfNotesInListView { get; set; }
        public bool SortAsscending { get; set; }
        public String DbTenantId { get; set; }

        //returns a Settings object with the default settings
        public static Settings DefaultSettings => new Settings
        {
            NumberOfNotesInListView = 10,
            SortAsscending = false,
            DbTenantId = String.Empty,
        };
    }
}
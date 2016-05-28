namespace NotesPCL.Model
{
    public class Settings
    {
        public int NumberOfNotesInListView { get; set; }

        //returns a Settings object with the default settings
        public static Settings DefaultSettings => new Settings
        {
            NumberOfNotesInListView = 10
        };
    }
}
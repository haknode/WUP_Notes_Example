using System.Collections.Generic;
using NotesPCL.Models;

namespace NotesPCL.Services
{
    //This interface defines how we access the data
    public interface IDataService
    {
        IEnumerable<Note> GetNotes();
        void AddNote(Note newNote);
        Settings GetSettings();
        void SetSettings(Settings newSettings);
    }
}

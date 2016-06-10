using System.Collections.Generic;
using NotesPCL.Models;

namespace NotesPCL.Services
{
    //This interface defines how we access the data
    public interface IDataService
    {
        IEnumerable<Note> GetNotes();
        Note GetNote();
        void AddNote(Note note);
        void RemoveNote(Note note);
        void RemoveAllNotes();
        Settings GetSettings();
        void SetSettings(Settings newSettings);
    }
}

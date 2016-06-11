using System;
using System.Collections.Generic;
using NotesPCL.Models;

namespace NotesPCL.Services
{
    //This interface defines how we access the data
    public interface IDataService
    {
        IEnumerable<Note> GetNotes();
        Note GetNote(Guid id);
        void AddOrUpdateNote(Note note);
        void RemoveNote(Guid id);
        void RemoveAllNotes();
        Settings GetSettings();
        void SetSettings(Settings newSettings);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesPCL.Models;

namespace NotesPCL.Services
{
    //This interface defines how we access the data
    public interface IDataService
    {
        Task<IEnumerable<Note>> GetNotes();
        Task<Note> GetNote(int id);
        Task AddNote(Note note);
        Task UpdateNote(Note note);
        Task RemoveNote(Note note);
        Task<Settings> GetSettings();
        Task SetSettings(Settings newSettings);

        Task SaveToStorage();
        Task LoadFromStorage();
    }
}

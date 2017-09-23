using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NotesPCL.Models;

namespace NotesPCL.Services
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    //This class provides default test data for our app
    public class MockDataService : IDataService
    {
        private static readonly String NOTES_STORAGE_STRING = "Notes";
        private static readonly String SETTINGS_STORAGE_STRING = "Settings";
        private static readonly String NEXIT_STORAGE_STRING = "NextId";

        private readonly IStorageService storageService;
        private readonly Dictionary<int, Note> notes;
        private Settings setting;

        private static int nextId = 0;

        public MockDataService(IStorageService storageService)
        {
            this.storageService = storageService;

            notes = new Dictionary<int, Note>();
            setting = Settings.DefaultSettings;
        }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            return notes.Values;
        }

        public async Task<Note> GetNote(int id)
        {
            return notes[id];
        }

        public async Task AddNote(Note note)
        {
            note.Id = nextId;
            nextId++;
            notes[note.Id] = note;
        }

        public async Task UpdateNote(Note note)
        {
            notes[note.Id] = note;
        }

        public async Task RemoveNote(Note note)
        {
            notes.Remove(note.Id);
        }

        public async Task<Settings> GetSettings()
        {
            return setting;
        }

        public async Task SetSettings(Settings newSettings)
        {
            setting = newSettings;
        }

        public async Task LoadFromStorage()
        {
            var loadedSettings = storageService.Read<Settings>(SETTINGS_STORAGE_STRING, Settings.DefaultSettings);
            await SetSettings(loadedSettings);

            var loadedNotes = storageService.Read<IEnumerable<Note>>(NOTES_STORAGE_STRING, new List<Note>());

            notes.Clear();
            foreach (var note in loadedNotes)
            {
                notes[note.Id] = note;
            }

            nextId = storageService.Read<int>(NEXIT_STORAGE_STRING);
        }

        public async Task SaveToStorage()
        {
            var settings = await GetSettings();
            storageService.Write<Settings>(SETTINGS_STORAGE_STRING, settings);
            var noteslist = await GetNotes();
            storageService.Write<IEnumerable<Note>>(NOTES_STORAGE_STRING, noteslist);
            storageService.Write<int>(NEXIT_STORAGE_STRING, nextId);

        }
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}
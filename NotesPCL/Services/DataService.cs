using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NotesPCL.Models;

namespace NotesPCL.Services
{
    //This class provides default test data for our app
    public class DataService : IDataService
    {
        private readonly IStorageService storageService;
        private readonly Dictionary<Guid, Note> notes;
        private Settings setting;

        public DataService(IStorageService storageService)
        {
            this.storageService = storageService;

            notes = new Dictionary<Guid, Note>();
            setting = Settings.DefaultSettings;

            InsertTestData(4);
        }

        public IEnumerable<Note> GetNotes()
        {
            return notes.Values;
        }

        public Note GetNote(Guid id)
        {
            return notes[id];
        }

        public void RemoveNote(Guid id)
        {
            notes.Remove(id);
        }

        public void RemoveAllNotes()
        {
            notes.Clear();
        }

        public void AddOrUpdateNote(Note newNote)
        {
            notes[newNote.Id] = newNote;
        }

        public Settings GetSettings()
        {
            return setting;
        }

        public void SetSettings(Settings newSettings)
        {
            setting = newSettings;
        }

        public void LoadFromStorage()
        {
            var loadedSettings = storageService.Read<Settings>("Settings", Settings.DefaultSettings);
            SetSettings(loadedSettings);

            var loadedNotes = storageService.Read<IEnumerable<Note>>("Notes", new List<Note>());

            RemoveAllNotes();
            foreach (var note in loadedNotes)
            {
                AddOrUpdateNote(note);
            }
        }

        public void SaveToStorage()
        {
            storageService.Write("Settings", GetSettings());
            storageService.Write("Notes", GetNotes());
        }

        private void InsertTestData(int num)
        {
            for (var i = 0; i < num; ++i)
            {
                AddOrUpdateNote(new Note
                                {
                                    Content = "This is Note " + i,
                                    Created = DateTime.Now.AddMonths(-1 * i),
                                });
            }
        }
    }
}
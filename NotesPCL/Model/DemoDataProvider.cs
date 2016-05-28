using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NotesPCL.Model
{
    //This class provides default test data for our app
    public class DemoDataProvider : IDataProvider
    {
        private List<Note> notes;
        private Settings setting;

        public DemoDataProvider()
        {
            notes = new List<Note>();
            setting = Settings.DefaultSettings;

            InsertTestData(4);
        }

        public IEnumerable<Note> GetNotes()
        {
            return this.notes;
        }

        public void AddNote(Note newNote)
        {
            notes.Add(newNote);
        }

        public Settings GetSettings()
        {
            return this.setting;
        }

        public void SetSettings(Settings newSettings)
        {
            this.setting = newSettings;
        }

        private void InsertTestData(int num)
        {
            for (var i = 0; i < num; ++i)
            {
                AddNote(new Note("This is Note " + i, DateTime.Now.AddMonths(-1*i)));
            }
        }
    }
}
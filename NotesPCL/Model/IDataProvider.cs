using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesPCL.Model
{
    //This interface defines how we access the data
    public interface IDataProvider
    {
        IEnumerable<Note> GetNotes();
        void AddNote(Note newNote);
        Settings GetSettings();
        void SetSettings(Settings newSettings);
    }
}

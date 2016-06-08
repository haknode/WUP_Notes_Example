using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using NotesPCL.Model;

namespace NotesPCL.ViewModel
{
    /* 
     * INPC is injected by Fody
     */
    public class ListViewModel : ViewModelBase
    {
        private readonly IDataProvider dataProvider;

        private readonly int numberOfDisplayedNotes;

        //Dependencies are injected by SimpleIOC
        public ListViewModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;

            numberOfDisplayedNotes = dataProvider.GetSettings().NumberOfNotesInListView;
        }

        //get the notes from the data provider and save them in a local copy
        //sort by creation date descending and take only the in the settings defined amount
        public IEnumerable<Note> Notes
            => dataProvider.GetNotes().OrderByDescending(n => n.Created).Take(numberOfDisplayedNotes);

        public string ListInfoText => $"Displaying the last {numberOfDisplayedNotes} Notes.";
    }
}
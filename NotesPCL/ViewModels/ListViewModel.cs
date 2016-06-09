using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using NotesPCL.Models;
using NotesPCL.Services;

namespace NotesPCL.ViewModels
{
    /* 
     * INPC is injected by Fody
     */
    public class ListViewModel : ViewModelBase
    {
        private readonly IDataService dataService;

        private readonly int numberOfDisplayedNotes;

        //Dependencies are injected by SimpleIOC
        public ListViewModel(IDataService dataService)
        {
            this.dataService = dataService;

            numberOfDisplayedNotes = dataService.GetSettings().NumberOfNotesInListView;
        }

        //get the notes from the data provider and save them in a local copy
        //sort by creation date descending and take only the in the settings defined amount
        public IEnumerable<Note> Notes
            => dataService.GetNotes().OrderByDescending(n => n.Created).Take(numberOfDisplayedNotes);

        public string ListInfoText => $"Displaying the last {numberOfDisplayedNotes} Notes.";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NotesPCL.Models;
using NotesPCL.Services;
using NotesPCL.Views;

namespace NotesPCL.ViewModels
{
    /* 
     * INPC is injected by Fody
     */
    public class ListViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;

        private int numberOfDisplayedNotes;
        private bool sortAscending;

        //Dependencies are injected by SimpleIOC
        public ListViewModel(INavigationService navigationService, IDataService dataService)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;
        }

        public void Load()
        {
            numberOfDisplayedNotes = dataService.GetSettings().NumberOfNotesInListView;
            sortAscending = dataService.GetSettings().SortAsscending;

            Notes = dataService.GetNotes();

            if (sortAscending)
                Notes = Notes.OrderBy(n => n.LastModified);
            else
                Notes = Notes.OrderByDescending(n => n.LastModified);

            Notes = Notes.Take(numberOfDisplayedNotes);
        }

        //get the notes from the data provider and save them in a local copy
        //sort by creation date descending and take only the in the settings defined amount
        public IEnumerable<Note> Notes { get; set; }

        public string ListInfoText
        {
            get
            {
                var sortString = sortAscending ? "ascending" : "descending";
                return $"Displaying the last {numberOfDisplayedNotes} Notes in {sortString} order.";
            }
        }

        public void EditNote(Note note)
        {
            navigationService.NavigateTo(ViewNames.EditPage, note.Id);
        }
    }
}
using System;
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

        private int numberOfDisplayedNotes;
        private bool sortAscending;

        //Dependencies are injected by SimpleIOC
        public ListViewModel(IDataService dataService)
        {
            this.dataService = dataService;

        }

        public void Load()
        {
            numberOfDisplayedNotes = dataService.GetSettings().NumberOfNotesInListView;
            sortAscending = dataService.GetSettings().SortAsscending;

            Notes = dataService.GetNotes();

            if (sortAscending)
                Notes = Notes.OrderBy(n => n.Created);
            else
                Notes = Notes.OrderByDescending(n => n.Created);

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
    }
}
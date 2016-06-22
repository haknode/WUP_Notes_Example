using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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

        public async void Load()
        {
            var settings = await dataService.GetSettings();
            numberOfDisplayedNotes = settings.NumberOfNotesInListView;
            sortAscending = settings.SortAsscending;

            //get the notes from the data provider and save them in a local copy
            //sort by creation date descending and take only the in the settings defined amount
            var notes = await dataService.GetNotes();

            if (sortAscending)
                notes = notes.OrderBy(n => n.LastModified);
            else
                notes = notes.OrderByDescending(n => n.LastModified);

            Notes = new ObservableCollection<Note>(notes.Take(numberOfDisplayedNotes));

            //when all notes are loaded, message the map to zoom to fit all pins
            Messenger.Default.Send<string>("zoomToFit");
        }

        public ObservableCollection<Note> Notes { get; set; }

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
            navigationService.NavigateTo(ViewNames.EditPage, note);
        }
    }
}
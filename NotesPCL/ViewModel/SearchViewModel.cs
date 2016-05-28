using System;
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
    public class SearchViewModel : ViewModelBase
    {
        private readonly IDataProvider dataProvider;

        private readonly List<Note> notes = new List<Note>();

        //Dependencies are injected by SimpleIOC
        public SearchViewModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;

            //get the notes from the data provider and save them in a local copy
            foreach (var note in dataProvider.GetNotes())
            {
                notes.Add(note);
            }

            //setup date selectors and disable them by default
            FromDate = DateTime.Now.AddDays(-1);
            ToDate = DateTime.Now;
            UseDateForSearch = false;
        }

        public string SearchString { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool UseDateForSearch { get; set; }

        public IEnumerable<Note> SearchResult
        {
            get
            {   //if no search filters are specified, show all notes
                IEnumerable<Note> result = notes;

                //if a search string is entered, filter the list
                if (!string.IsNullOrEmpty(SearchString))
                    result = result.Where(note => note.Content.ToLower().Contains(SearchString.ToLower()));

                //if filter by date is enabled 
                if (UseDateForSearch && FromDate != null)
                    result = result.Where(note => note.Created.Date.CompareTo(FromDate.Date) >= 0);

                //if filter by date is enabled
                if (UseDateForSearch && ToDate != null)
                    result = result.Where(note => note.Created.Date.CompareTo(ToDate.Date) <= 0);

                return result;
            }
        }
    }
}
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
    public class SearchViewModel : ViewModelBase
    {
        private readonly IDataService dataService;

        //Dependencies are injected by SimpleIOC
        public SearchViewModel(IDataService dataService)
        {
            this.dataService = dataService;

            ClearSearch();
        }

        public string SearchString { get; set; }

        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset FromDateMaxValue => ToDate ?? DateTimeOffset.MaxValue;

        public DateTimeOffset? ToDate { get; set; }
        public DateTimeOffset ToDateMinValue => FromDate ?? DateTimeOffset.MinValue;

        public IEnumerable<Note> SearchResult
        {
            get
            {
                return dataService.GetNotes().Where(note =>
                {
                    if (!string.IsNullOrEmpty(SearchString)
                        && !note.Content.ToLower().Contains(SearchString.ToLower()))
                        return false;

                    if (FromDate.HasValue 
                        && note.Created.Date.CompareTo(FromDate.Value.Date) < 0)
                        return false;

                    if (ToDate.HasValue 
                        && note.Created.Date.CompareTo(ToDate.Value.Date) > 0)
                        return false;

                    return true;    //return all elements by default
                });
            }
        }

        public void ClearSearch()
        {
            //setup date selectors as empty and disable them by default
            SearchString = String.Empty;
            FromDate = null;
            ToDate = null;
        }
    }
}
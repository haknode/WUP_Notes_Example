﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class SearchViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly IDataService dataService;

        //Dependencies are injected by SimpleIOC
        public SearchViewModel(INavigationService navigationService, IDataService dataService)
        {
            this.navigationService = navigationService;
            this.dataService = dataService;

            PropertyChanged += UpdateSearchResultList;

            ClearSearch();
        }

        private async void UpdateSearchResultList(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchString) || 
                e.PropertyName == nameof(FromDate) ||
                e.PropertyName == nameof(ToDate))
            {
                var notes = await dataService.GetNotes();
                SearchResult = notes.Where(note =>
                {
                    if (!string.IsNullOrEmpty(SearchString)
                        && !note.Content.ToLower().Contains(SearchString.ToLower()))
                        return false;

                    if (FromDate.HasValue
                        && note.LastModified.Date.CompareTo(FromDate.Value.Date) < 0)
                        return false;

                    if (ToDate.HasValue
                        && note.LastModified.Date.CompareTo(ToDate.Value.Date) > 0)
                        return false;

                    return true;    //return all elements by default
                });
            }
            
        }

        public string SearchString { get; set; }

        public DateTimeOffset? FromDate { get; set; }
        public DateTimeOffset FromDateMaxValue => ToDate ?? DateTimeOffset.MaxValue;

        public DateTimeOffset? ToDate { get; set; }
        public DateTimeOffset ToDateMinValue => FromDate ?? DateTimeOffset.MinValue;

        public IEnumerable<Note> SearchResult { get; set; }


        public void ClearSearch()
        {
            //setup date selectors as empty and disable them by default
            SearchString = String.Empty;
            FromDate = null;
            ToDate = null;
        }

        public void EditNote(Note note)
        {
            navigationService.NavigateTo(ViewNames.EditPage, note.Id);
        }
    }
}
﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NotesPCL.Models;
using NotesPCL.ViewModels;

namespace Notes.Views
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public SearchViewModel ViewModel => (SearchViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.Back)
            {
                ViewModel.ClearSearch();
            }

            ViewModel.LoadNotes();

            base.OnNavigatedTo(e);
        }

        private void NotesListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var note = (Note)e.ClickedItem;

            ViewModel.EditNote(note);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NotesPCL.Models;
using NotesPCL.Services;
using PropertyChanged;

namespace NotesPCL.ViewModels
{
    /* 
     * INPC is injected by Fody
     */
    public class EditViewModel : ViewModelBase
    {
        private readonly IDataService dataService;
        private readonly IDialogService dialogService;
        private readonly ILocationService locationService;
        private readonly INavigationService navigationService;

        private Note originalNote;

        private CancellationTokenSource cancellationTokenSource;

        //Dependencies are injected by SimpleIOC
        public EditViewModel(IDataService dataService, INavigationService navigationService, IDialogService dialogService, ILocationService locationService)
        {
            this.dataService = dataService;
            this.dialogService = dialogService;
            this.locationService = locationService;
            this.navigationService = navigationService;

            Clear();
        }

        public Note EditNote { get; private set; }

        //ObservableCollection is needed here because the mapcontrol expects one
        public ObservableCollection<Note> AllNotes => new ObservableCollection<Note>(dataService.GetNotes());

        public double ZoomLevel { get; set; } = 13;

        public Boolean CanSave => !string.IsNullOrWhiteSpace(EditNote?.Content) && EditNote?.Content != originalNote?.Content;

        public Boolean CanDelete => originalNote != null;

        public void SaveNote()
        {
            //if the note is not empty, save it and navigate back
            if (CanSave)
            {
                EditNote.LastModified = DateTime.Now;

                dataService.AddOrUpdateNote(EditNote);

                ClearAndGoBack();
            }
        }

        public async void Cancel()
        {
            //if the note is empty, go back without the showing the confirm dialog
            if (CanSave)
            {
                //show a dialog to confirm 
                var confirmed = await dialogService.ShowMessage("Your note was not saved! Go back without saving?", "Continue without Saving?",
                    "Continue", "Cancel", isOkPressed => { /* Do Nothing */ });

                if (!confirmed)
                {
                    return;
                }
            }

            ClearAndGoBack();
        }
        public void LoadExistingNote(Guid id)
        {
            Clear();

            //get the note but use a copy to edit
            originalNote = dataService.GetNote(id);
            var clonedNote = originalNote.Clone();
            
            LoadNote(clonedNote);
        }
        private Random random = new Random();
        public void LoadEmptyNote()
        {
            Clear();

            LoadNote(new Note());
            //Try to get the position
            TryGetPosition();
        }

        private void LoadNote(Note note)
        {
            EditNote = note;

            EditNote.PropertyChanged += (sender, args) =>
            {
                RaisePropertyChanged(() => CanSave);
            };
        }

        public async void DeleteNote()
        {
            if (EditNote != null)
            {
                var confirmed = await dialogService.ShowMessage("Do you really want to delete this Note?", "Delete Note?",
                    "Delete", "Cancel", isOkPressed => { /* Do Nothing */ });

                if (confirmed)
                {
                    dataService.RemoveNote(EditNote.Id);
                    ClearAndGoBack();
                }
            }
        }

        private void ClearAndGoBack()
        {
            Clear();

            navigationService.GoBack();
        }

        private void Clear()
        {
            EditNote = null;
            originalNote = null;

            cancellationTokenSource?.Cancel(); //Cancel GetCurrentLocation operation if any
        }

        private async void TryGetPosition()
        {
            cancellationTokenSource?.Cancel();  //Cancel GetCurrentLocation if any
            cancellationTokenSource = new CancellationTokenSource();    //Creat new cancellatoinToken

            //Get Location
            EditNote.CreationLocation = await locationService.GetCurrentLocationAsync(cancellationTokenSource.Token);
        }
    }
}
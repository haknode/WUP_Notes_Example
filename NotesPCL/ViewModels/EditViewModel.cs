using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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
        public ObservableCollection<Note> AllNotes { get; set; }

        public double ZoomLevel => IsNewNote ? 0 : 13;

        public Boolean CanSave => !string.IsNullOrWhiteSpace(EditNote?.Content) && EditNote?.Content != originalNote?.Content;

        public Boolean CanDelete => originalNote != null;

        private Boolean IsNewNote => originalNote == null;

        public async void SaveNote()
        {
            //if the note is not empty, save it and navigate back
            if (CanSave)
            {
                EditNote.LastModified = DateTime.Now;

                if (IsNewNote)   //new note
                {
                    await dataService.AddNote(EditNote);
                }
                else
                {
                    await dataService.UpdateNote(EditNote);
                }

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
        public void LoadExistingNote(Note note)
        {
            Clear();

            //get the note but use a copy to edit
            //originalNote = await dataService.GetNote(note.Id);
            originalNote = note;
            var clonedNote = originalNote.Clone();

            LoadNote(clonedNote);
        }

        public void LoadEmptyNote()
        {
            Clear();

            LoadNote(new Note());

            TryGetPosition();
        }

        private async void LoadNote(Note note)
        {
            EditNote = note;

            //TODO: to slow
            var allNotes = await dataService.GetNotes();
            if(allNotes == null)
                return;

            AllNotes = new ObservableCollection<Note>(allNotes);

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
                    await dataService.RemoveNote(EditNote);
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
            if(EditNote.CreationLocation != null && EditNote.CreationLocation.IsValid)
                Messenger.Default.Send<string>("centerToCurrentLocation");
        }
    }
}
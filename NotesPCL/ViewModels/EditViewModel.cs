using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NotesPCL.Models;
using NotesPCL.Services;

namespace NotesPCL.ViewModels
{
    /* 
     * INPC is injected by Fody
     */
    public class EditViewModel : ViewModelBase
    {
        private readonly IDataService dataService;
        private readonly IDialogService dialogService;
        private readonly INavigationService navigationService;

        //If this variable references a note, we are in edit mode
        private Note editNote;

        //Dependencies are injected by SimpleIOC
        public EditViewModel(IDataService dataService, INavigationService navigationService, IDialogService dialogService)
        {
            this.dataService = dataService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            ClearAndGoBack();
        }

        public DateTime CreationDateTime { get; set; }

        public string Content { get; set; }

        public Boolean CanSave => !(string.IsNullOrWhiteSpace(Content) || (editNote != null && Content == editNote.Content));
        public Boolean CanDelete => editNote != null;

        public void SaveNote()
        {
            //if the note is not empty, save it and navigate back
            if (CanSave)
            {
                if (editNote == null)
                    editNote = new Note();

                editNote.Content = Content;
                editNote.LastModified = DateTime.Now;

                dataService.AddOrUpdateNote(editNote);

                ClearAndGoBack();
            }
        }

        public async void Cancel()
        {
            //if the note is empty, go back without the showing the confirm dialog
            if (!CanSave)
            {
                ClearAndGoBack();
            }
            else
            {   //show a dialog to confirm 
                var confirmed = await dialogService.ShowMessage("Your note was not saved! Go back without saving?", "Continue without Saving?",
                    "Continue", "Cancel", isOkPressed => { /* Do Nothing */ });

                if (confirmed)
                {
                    ClearAndGoBack();
                }
            }
        }
        public void LoadNote(Guid id)
        {
            editNote = dataService.GetNote(id);

            Content = editNote.Content;
            CreationDateTime = editNote.LastModified;
        }

        public async void DeleteNote()
        {
            if (editNote != null)
            {
                var confirmed = await dialogService.ShowMessage("Do you really want to delete this Note?", "Delete Note?",
                    "Delete", "Cancel", isOkPressed => { /* Do Nothing */ });

                if (confirmed)
                {
                    dataService.RemoveNote(editNote.Id);
                    ClearAndGoBack();
                }
            }
        }

        private void ClearAndGoBack()
        {
            Content = string.Empty;
            CreationDateTime = DateTime.Now;
            editNote = null;

            navigationService.GoBack();
        }
    }
}
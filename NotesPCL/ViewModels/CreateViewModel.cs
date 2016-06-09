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
    public class CreateViewModel : ViewModelBase
    {
        private readonly IDataService dataService;
        private readonly IDialogService dialogService;
        private readonly INavigationService navigationService;

        //Dependencies are injected by SimpleIOC
        public CreateViewModel(IDataService dataService, INavigationService navigationService, IDialogService dialogService)
        {
            this.dataService = dataService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            ClearAndGoBack();
        }

        public DateTime CreationDateTime { get; set; }

        public string Content { get; set; }

        public Boolean CanSave => !string.IsNullOrWhiteSpace(Content);

        public void SaveNote()
        {
            //if the note is not empty, save it and navigate back
            if (CanSave)
            {
                dataService.AddNote(new Note(Content, CreationDateTime));

                ClearAndGoBack();
            }
        }

        public async void Cancel()
        {
            //if the note is empty, go back without the showing the confirm dialog
            if (string.IsNullOrWhiteSpace(Content))
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

        private void ClearAndGoBack()
        {
            Content = string.Empty;
            CreationDateTime = DateTime.Now;

            navigationService.GoBack();
        }
    }
}
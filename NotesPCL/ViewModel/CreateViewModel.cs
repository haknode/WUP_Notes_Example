using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NotesPCL.Model;

namespace NotesPCL.ViewModel
{
    /* 
     * INPC is injected by Fody
     */
    public class CreateViewModel : ViewModelBase
    {
        private readonly IDataProvider dataProvider;
        private readonly IDialogService dialogService;
        private readonly INavigationService navigationService;

        //Dependencies are injected by SimpleIOC
        public CreateViewModel(IDataProvider dataProvider, INavigationService navigationService, IDialogService dialogService)
        {
            this.dataProvider = dataProvider;
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            Clear();
        }

        public DateTime CreationDateTime { get; set; }

        public string Content { get; set; }

        public void SaveNote()
        {
            //if the note is not empty, save it and navigate back
            if (!string.IsNullOrWhiteSpace(Content))
            {
                dataProvider.AddNote(new Note(Content, CreationDateTime));

                Clear();

                navigationService.GoBack();
            }
        }

        public void Cancel()
        {
            //if the note is empty, go back without the showing the confirm dialog
            if (string.IsNullOrWhiteSpace(Content))
            {
                Clear();
                navigationService.GoBack();
            }
            else
            {   //show a dialog to confirm 
                dialogService.ShowMessage("Your note was not saved! Go back without saving?", "Continue without Saving?",
                    "Continue", "Cancel", confirmed =>
                    {
                        if (confirmed)
                        {
                            Clear();
                            navigationService.GoBack();
                        }
                    });
            }
        }

        private void Clear()
        {
            Content = string.Empty;
            CreationDateTime = DateTime.Now;
        }
    }
}
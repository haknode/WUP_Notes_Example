using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NotesPCL.Model;

namespace NotesPCL.ViewModel
{
    public class CreateViewModel : ViewModelBase
    {
        private readonly IDialogService dialogService;
        private readonly INavigationService navigationService;

        public CreateViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            Clear();
        }

        public DateTime Now { get; set; }

        public String Content { get; set; }

        public void SaveNote()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                DataStorage.Notes.Add(new Note(Content, Now));

                Clear();

                navigationService.GoBack();
            }
                
        }

        public void Cancel()
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                Clear();
                navigationService.GoBack();
            }
            else
            {
                dialogService.ShowMessage("Your note was not saved! Go back without saving?", "Continue without Saving?", "Continue", "Cancel", (confirmed) =>
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
            Content = String.Empty;
            Now = DateTime.Now;
        }
    }
}

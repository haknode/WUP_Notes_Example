using GalaSoft.MvvmLight.Views;
using Notes.Views;
using NotesPCL.Views;
using NotesPCL.ViewModels;

namespace Notes.ViewModels
{
    public class WupViewModelLocator : ViewModelLocator
    {
        public override INavigationService GenerateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(ViewModelNames.CreatePage, typeof(CreatePage));
            navigationService.Configure(ViewModelNames.ListPage, typeof(ListPage));
            navigationService.Configure(ViewModelNames.SearchPage, typeof(SearchPage));
            navigationService.Configure(ViewModelNames.SettingsPage, typeof(SettingsPage));
            navigationService.Configure(ViewModelNames.StartPage, typeof(StartPage));

            return navigationService;
        }

        public override IDialogService GenerateDialogService()
        {
            return new DialogService();
        }
    }
}
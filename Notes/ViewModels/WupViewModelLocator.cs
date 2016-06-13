using GalaSoft.MvvmLight.Views;
using Notes.Views;
using NotesPCL.Views;
using NotesPCL.ViewModels;
using NotesPCL.Services;
using GalaSoft.MvvmLight.Ioc;
using Notes.Services;

namespace Notes.ViewModels
{
    public class WupViewModelLocator : ViewModelLocator
    {
        public WupViewModelLocator() : base()
        {
            //This NavigationService should be injected for every INavigationService
            SimpleIoc.Default.Register<INavigationService>(GenerateNavigationService);

            /* Possible warning: "There is already a factory registered for GalaSoft.MvvmLight.Views.INavigationService."
             * This is because VisualStudio is also registering the INavigationService.
             */

            //This DialogService should be injected for every IDialogService
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            //This StorageService should be injected for every IStorageService
            SimpleIoc.Default.Register<IStorageService, WupLocalSettingsStorageService>();
        }
        public INavigationService GenerateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(ViewNames.CreatePage, typeof(CreatePage));
            navigationService.Configure(ViewNames.ListPage, typeof(ListPage));
            navigationService.Configure(ViewNames.SearchPage, typeof(SearchPage));
            navigationService.Configure(ViewNames.SettingsPage, typeof(SettingsPage));
            navigationService.Configure(ViewNames.StartPage, typeof(StartPage));

            return navigationService;
        }
    }
}
using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;

namespace NotesPCL.ViewModel
{
    public class StartViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public StartViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public void NavigateToCreatePage()
        {
            navigationService.NavigateTo(ViewModelNames.CreatePage);
        }

        public void NavigateToListPage()
        {
            navigationService.NavigateTo(ViewModelNames.ListPage);
        }

        public void NavigateToSearchPage()
        {
            navigationService.NavigateTo(ViewModelNames.SearchPage);
        }

        public void NavigateToSettingsPage()
        {
            navigationService.NavigateTo(ViewModelNames.SettingsPage);
        }
    }
}

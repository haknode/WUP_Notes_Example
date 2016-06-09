using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using NotesPCL.Views;

namespace NotesPCL.ViewModels
{
    /* 
     * INPC is injected by Fody
     */
    public class StartViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        //Dependencies are injected by SimpleIOC
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
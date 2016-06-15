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
            navigationService.NavigateTo(ViewNames.EditPage);
        }

        public void NavigateToListPage()
        {
            navigationService.NavigateTo(ViewNames.ListPage);
        }

        public void NavigateToSearchPage()
        {
            navigationService.NavigateTo(ViewNames.SearchPage);
        }

        public void NavigateToSettingsPage()
        {
            navigationService.NavigateTo(ViewNames.SettingsPage);
        }
    }
}
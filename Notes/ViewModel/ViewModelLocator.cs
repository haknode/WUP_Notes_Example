using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Notes.View;
using NotesPCL.ViewModel;

namespace Notes.ViewModel
{
    public class ViewModelLocator
    {

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = new NavigationService();
            navigationService.Configure(ViewModelNames.CreatePage, typeof(CreatePage));
            navigationService.Configure(ViewModelNames.ListPage, typeof(ListPage));
            navigationService.Configure(ViewModelNames.SearchPage, typeof(SearchPage));
            navigationService.Configure(ViewModelNames.SettingsPage, typeof(SettingsPage));
            navigationService.Configure(ViewModelNames.StartPage, typeof(StartPage));

            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            SimpleIoc.Default.Register<IDialogService, DialogService>();

            SimpleIoc.Default.Register<StartViewModel>();
            SimpleIoc.Default.Register<CreateViewModel>();
            SimpleIoc.Default.Register<ListViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();


        }

        public StartViewModel StartViewModel => ServiceLocator.Current.GetInstance<StartViewModel>();
        public CreateViewModel CreateViewModel => ServiceLocator.Current.GetInstance<CreateViewModel>();
        public ListViewModel ListViewModel => ServiceLocator.Current.GetInstance<ListViewModel>();
        public SearchViewModel SearchViewModel => ServiceLocator.Current.GetInstance<SearchViewModel>();
        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
    }
}

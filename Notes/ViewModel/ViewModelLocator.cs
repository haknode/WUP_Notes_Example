using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Notes.View;
using NotesPCL.Model;
using NotesPCL.View;
using NotesPCL.ViewModel;

namespace Notes.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            //Setup IOC Container
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Create a NavigationService and register the pages with their corresponding names
            var navigationService = new NavigationService();
            navigationService.Configure(ViewModelNames.CreatePage, typeof(CreatePage));
            navigationService.Configure(ViewModelNames.ListPage, typeof(ListPage));
            navigationService.Configure(ViewModelNames.SearchPage, typeof(SearchPage));
            navigationService.Configure(ViewModelNames.SettingsPage, typeof(SettingsPage));
            navigationService.Configure(ViewModelNames.StartPage, typeof(StartPage));

            //This NavigationService should be injected for every INavigationService
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            //The default DialogService should be injected for every IDialogService
            SimpleIoc.Default.Register<IDialogService, DialogService>();

            //DemoDataProvider should be injected for every IDataProvider
            SimpleIoc.Default.Register<IDataProvider, DemoDataProvider>();

            //Register the ViewModels
            SimpleIoc.Default.Register<StartViewModel>();
            SimpleIoc.Default.Register<CreateViewModel>();
            SimpleIoc.Default.Register<ListViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        /*
         * Getters for the ViewModels. ListViewModel and SearchViewModel return a new instance every time.
         * The others always return the same insatance.
         */
        public StartViewModel StartViewModel => SimpleIoc.Default.GetInstance<StartViewModel>();
        public CreateViewModel CreateViewModel => SimpleIoc.Default.GetInstance<CreateViewModel>();
        public ListViewModel ListViewModel => SimpleIoc.Default.GetInstance<ListViewModel>(Guid.NewGuid().ToString());
        public SearchViewModel SearchViewModel => SimpleIoc.Default.GetInstance<SearchViewModel>(Guid.NewGuid().ToString());
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>(Guid.NewGuid().ToString());
    }
}
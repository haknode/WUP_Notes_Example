using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using NotesPCL.Services;

namespace NotesPCL.ViewModels
{
    public abstract class ViewModelLocator
    {
        protected ViewModelLocator()
        {
            //Setup IOC Container
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Create a NavigationService and register the pages with their corresponding names
            //This NavigationService should be injected for every INavigationService
            SimpleIoc.Default.Register<INavigationService>(GenerateNavigationService);

            /* Possible warning: "There is already a factory registered for GalaSoft.MvvmLight.Views.INavigationService."
             * This is because VisualStudio is creating the INavigationService.
             */

            //The default DialogService should be injected for every IDialogService
            SimpleIoc.Default.Register<IDialogService>(GenerateDialogService);

            //DemoDataService should be injected for every IDataService
            SimpleIoc.Default.Register<IDataService, DemoDataService>();

            //Register the ViewModels
            SimpleIoc.Default.Register<StartViewModel>();
            SimpleIoc.Default.Register<CreateViewModel>();
            SimpleIoc.Default.Register<ListViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        //The platform depending code needs to implement these
        public abstract INavigationService GenerateNavigationService();
        public abstract IDialogService GenerateDialogService();

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
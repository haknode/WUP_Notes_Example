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

            //DataService should be injected for every IDataService
            SimpleIoc.Default.Register<IDataService, DataService>();

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
        //TODO: donte return a new instance every time
        public StartViewModel StartViewModel => ServiceLocator.Current.GetInstance<StartViewModel>();
        public CreateViewModel CreateViewModel => ServiceLocator.Current.GetInstance<CreateViewModel>();
        public ListViewModel ListViewModel => ServiceLocator.Current.GetInstance<ListViewModel>(Guid.NewGuid().ToString());
        public SearchViewModel SearchViewModel => ServiceLocator.Current.GetInstance<SearchViewModel>(Guid.NewGuid().ToString());
        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>(Guid.NewGuid().ToString());
    }
}
using System;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using NotesPCL.Services;

namespace NotesPCL.ViewModels
{
    public abstract class ViewModelLocator
    {
        static ViewModelLocator()
        {
            //Setup IOC Container
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////MockDataService should be injected for every IDataService
            //SimpleIoc.Default.Register<IDataService, MockDataService>();

            //RestDataService should be injected for every IDataService
            SimpleIoc.Default.Register<IDataService, RestDataService>();

            //Register the ViewModels
            SimpleIoc.Default.Register<StartViewModel>();
            SimpleIoc.Default.Register<EditViewModel>();
            SimpleIoc.Default.Register<ListViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        /*
         * Getters for the ViewModels. ListViewModel and SearchViewModel return a new instance every time.
         * The others always return the same insatance.
         */
        public StartViewModel StartViewModel => ServiceLocator.Current.GetInstance<StartViewModel>();
        public EditViewModel EditViewModel => ServiceLocator.Current.GetInstance<EditViewModel>();
        public ListViewModel ListViewModel => ServiceLocator.Current.GetInstance<ListViewModel>();
        public SearchViewModel SearchViewModel => ServiceLocator.Current.GetInstance<SearchViewModel>();
        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();
    }
}
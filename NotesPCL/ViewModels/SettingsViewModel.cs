using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using NotesPCL.Models;
using NotesPCL.Services;

namespace NotesPCL.ViewModels
{
    /* 
     * INPC is injected by Fody
     */
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IDataService dataService;

        //Dependencies are injected by SimpleIOC
        public SettingsViewModel(IDataService dataService)
        {
            this.dataService = dataService;

            Settings settings = dataService.GetSettings();

            NumberOfNotesInListView = settings.NumberOfNotesInListView.ToString();

            SortingOrdersList = new List<string>()
            {
                "Ascending",
                "Descending",
            };
        }

        public String NumberOfNotesInListView { get; set; }

        public List<String> SortingOrdersList { get; }

        public String SortingOrder { get; set; }

        private bool IsNumberOfNotesInListViewValid(String value)
        {
            int intValue;

            if (int.TryParse(value, out intValue) == false)
                return false;

            if (intValue < 0)
                return false;

            return true;
        }

        public void SaveSettings()
        {
            var newSettings = new Settings();

            if (IsNumberOfNotesInListViewValid(NumberOfNotesInListView))
                newSettings.NumberOfNotesInListView = int.Parse(NumberOfNotesInListView);
            else
                newSettings.NumberOfNotesInListView = Settings.DefaultSettings.NumberOfNotesInListView;

            dataService.SetSettings(newSettings);
        }
    }
}
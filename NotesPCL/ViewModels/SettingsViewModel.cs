using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
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

            SortingOrdersList = new string[]
            {
                "Ascending",
                "Descending",
            };
        }

        public String NumberOfNotesInListView { get; set; }

        public String[] SortingOrdersList { get; }

        public int SortingOrder { get; set; }

        private bool IsNumberOfNotesInListViewValid()
        {
            int intValue;

            if (int.TryParse(NumberOfNotesInListView, out intValue) == false)
                return false;

            if (intValue < 0)
                return false;

            return true;
        }

        private bool IsSortingOrderValid()
        {
            return SortingOrder >= 0 && SortingOrder <= 1;
        }

        public void Load()
        {
            Settings settings = dataService.GetSettings();

            NumberOfNotesInListView = settings.NumberOfNotesInListView.ToString();
            SortingOrder = settings.SortAsscending ? 0 : 1;
        }

        public void Save()
        {
            if (IsNumberOfNotesInListViewValid() && IsSortingOrderValid())
            {
                var newSettings = new Settings
                {
                    NumberOfNotesInListView = int.Parse(NumberOfNotesInListView),
                    SortAsscending = SortingOrder == 0
                };

                dataService.SetSettings(newSettings);
            }
        }

        public void LoadNotesFromStorage()
        {
            dataService.LoadFromStorage();
        }

        public void SaveNotesToStorage()
        {
            dataService.SaveToStorage();
        }
    }
}
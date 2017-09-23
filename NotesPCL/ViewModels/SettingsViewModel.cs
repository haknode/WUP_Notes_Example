using System;
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

            SortingOrdersNames = new string[]
            {
                "Ascending",
                "Descending",
            };
        }

        public String NumberOfNotesInListView { get; set; }

        public String[] SortingOrdersNames { get; }

        public int SortingOrder { get; set; }

        public String TenantId { get; set; }

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

        private bool IsTenantIdValid()
        {
            return !String.IsNullOrWhiteSpace(TenantId);
        }

        public async void Load()
        {
            var settings = await dataService.GetSettings();

            NumberOfNotesInListView = settings.NumberOfNotesInListView.ToString();
            SortingOrder = settings.SortAsscending ? 0 : 1;
            TenantId = settings.DbTenantId;
        }

        public void Save()
        {
            if (IsNumberOfNotesInListViewValid() && IsSortingOrderValid() && IsTenantIdValid())
            {
                var newSettings = new Settings
                {
                    NumberOfNotesInListView = int.Parse(NumberOfNotesInListView),
                    SortAsscending = SortingOrder == 0,
                    DbTenantId = TenantId,
                };

                dataService.SetSettings(newSettings);
            }
        }

        public void LoadNotesFromStorage()
        {
            dataService.LoadFromStorage();
            Load();
        }

        public void SaveNotesToStorage()
        {
            Save();
            dataService.SaveToStorage();
        }
    }
}
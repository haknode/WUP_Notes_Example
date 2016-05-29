using System;
using GalaSoft.MvvmLight;
using NotesPCL.Model;

namespace NotesPCL.ViewModel
{
    /* 
     * INPC is injected by Fody
     */
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IDataProvider dataProvider;

        private String numberOfNotesInListView;


        //Dependencies are injected by SimpleIOC
        public SettingsViewModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;

            Settings settings = dataProvider.GetSettings();

            NumberOfNotesInListView = settings.NumberOfNotesInListView.ToString();
        }

        public String NumberOfNotesInListView { get; set; }

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

            dataProvider.SetSettings(newSettings);
        }
    }
}
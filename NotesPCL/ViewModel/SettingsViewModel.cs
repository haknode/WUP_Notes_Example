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

        private Settings settings;

        //Dependencies are injected by SimpleIOC
        public SettingsViewModel(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;

            settings = dataProvider.GetSettings();
        }

        public int NumberOfNotesInListView
        {
            get
            {
                return settings.NumberOfNotesInListView;
            }
            set
            {
                if (value == settings.NumberOfNotesInListView)
                {
                    settings.NumberOfNotesInListView = value;
                    SaveSettings();
                }
            }
        }

        private void SaveSettings()
        {
            dataProvider.SetSettings(new Settings()
            {
                 NumberOfNotesInListView = NumberOfNotesInListView,
            });
        }
    }
}
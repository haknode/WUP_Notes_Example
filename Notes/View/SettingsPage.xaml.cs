using Windows.UI.Xaml.Controls;
using NotesPCL.ViewModel;

namespace Notes.View
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public SettingsViewModel ViewModel => (SettingsViewModel) DataContext;
    }
}
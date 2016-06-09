using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NotesPCL.ViewModels;

namespace Notes.Views
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public SettingsViewModel ViewModel => (SettingsViewModel) DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Load();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.Save(); //save the new settings;
        }
    }
}
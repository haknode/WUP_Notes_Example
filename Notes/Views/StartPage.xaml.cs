using Windows.UI.Xaml.Controls;
using NotesPCL.ViewModels;

namespace Notes.Views
{
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        public StartViewModel ViewModel => (StartViewModel) DataContext;
    }
}
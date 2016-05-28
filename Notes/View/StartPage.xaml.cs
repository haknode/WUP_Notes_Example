using Windows.UI.Xaml.Controls;
using NotesPCL.ViewModel;

namespace Notes.View
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
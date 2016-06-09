using Windows.UI.Xaml.Controls;
using NotesPCL.ViewModels;

namespace Notes.Views
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        public SearchViewModel ViewModel => (SearchViewModel) DataContext;
    }
}
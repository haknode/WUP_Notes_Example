using Windows.UI.Xaml.Controls;
using NotesPCL.ViewModel;

namespace Notes.View
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
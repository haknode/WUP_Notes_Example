using Windows.UI.Xaml.Controls;
using NotesPCL.ViewModels;

namespace Notes.Views
{
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            InitializeComponent();
        }

        public ListViewModel ViewModel => (ListViewModel) DataContext;
    }
}
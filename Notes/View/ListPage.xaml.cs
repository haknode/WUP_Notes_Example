using Windows.UI.Xaml.Controls;
using NotesPCL.ViewModel;

namespace Notes.View
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
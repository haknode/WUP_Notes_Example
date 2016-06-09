using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Load();
            base.OnNavigatedTo(e);
        }
    }
}
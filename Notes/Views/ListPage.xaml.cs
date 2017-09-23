using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using NotesPCL.Models;
using NotesPCL.ViewModels;

namespace Notes.Views
{
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            InitializeComponent();
        }

        public ListViewModel ViewModel => (ListViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.Load();

            base.OnNavigatedTo(e);
        }

        private void NotesListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var note = (Note)e.ClickedItem;

            ViewModel.EditNote(note);
        }

        private void NotesMap_OnMapPinClicked(object sender, Note note)
        {
            ViewModel.EditNote(note);
        }
    }
}
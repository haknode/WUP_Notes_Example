using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using NotesPCL.Model;

namespace NotesPCL.ViewModel
{
    public class ListViewModel : ViewModelBase
    {

        public ListViewModel()
        {
            Notes = DataStorage.Notes;
        }
        public ObservableCollection<Note> Notes { get; set; }

        public String ListInfoText => $"Displaying the last {DataStorage.Settings.NumberOfNotesInListView} Notes.";
    }
}

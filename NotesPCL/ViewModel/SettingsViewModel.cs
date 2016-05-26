using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace NotesPCL.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            NumberOfNotesInListView = 5;
        }

        public int NumberOfNotesInListView { get; set; }
    }
}

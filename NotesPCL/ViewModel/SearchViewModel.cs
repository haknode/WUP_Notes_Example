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
    public class SearchViewModel : ViewModelBase
    {
        public SearchViewModel()
        {
            Notes = DataStorage.Notes;

            FromDate = DateTime.Now.AddDays(-1);
            ToDate = DateTime.Now;
            UseDateForSearch = false;
        }

        public String SearchString { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public bool UseDateForSearch { get; set; }

        private ObservableCollection<Note> Notes { get; set; }

        public IEnumerable<Note> SearchResult
        {
            get
            {
                IEnumerable<Note> result = Notes;

                if (!String.IsNullOrEmpty(SearchString))
                    result = result.Where((note) => note.Content.Contains(SearchString));

                if (UseDateForSearch && FromDate != null)
                    result = result.Where((note) => note.Created.Date.CompareTo(FromDate.Date) >= 0);

                if (UseDateForSearch && ToDate != null)
                    result = result.Where((note) => note.Created.Date.CompareTo(ToDate.Date) <= 0);

                return result;
            }
        }
    }
}

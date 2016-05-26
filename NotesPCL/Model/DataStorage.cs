using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesPCL.Model
{
    public class DataStorage
    {
        static DataStorage()
        {
            Notes = new ObservableCollection<Note>();
            Settings = Settings.DefaultSettings;

            InsertTestData(4);
        }

        public static ObservableCollection<Note> Notes { get; set; }

        public static Settings Settings { get; set; }

        private static void InsertTestData(int num)
        {
            for (int i = 0; i < num; ++i)
            {
                Notes.Add(new Note("This is Note " + i, DateTime.Now.AddMonths(-1*i)));
            }
        }
    }
}

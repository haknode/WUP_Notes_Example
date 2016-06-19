using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ServiceLocation;
using Notes.Converters;
using NotesPCL.Models;
using NotesPCL.Services;
using NotesPCL.ViewModels;

namespace Notes.Views
{
    public sealed partial class EditPage : Page
    {
        private readonly DispatcherTimer dispatcherTimer;

        public EditPage()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();    //DispatcherTimer to periodically update the current time
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (sender, o) => { ViewModel.EditNote.LastModified = DateTime.Now; };   //Update the time in the ViewModel every 1 second
        }

        public EditViewModel ViewModel => (EditViewModel)DataContext;

        //This method is executed on every BackRequested event
        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;   //this event is now handled
            ViewModel.Cancel(); //the cancel method executes the navigate back
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //when the user navigates to this page
            //register the OnBackRequested method to the event
            ((App)Application.Current).OnBackRequested += OnBackRequested;

            var note = e.Parameter as Note;
            if (note != null)
            {
                ViewModel.LoadExistingNote(note);
            }
            else
            {
                ViewModel.LoadEmptyNote();
                dispatcherTimer.Start();
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //when the user navigates away from this page
            ((App)Application.Current).OnBackRequested -= OnBackRequested;

            dispatcherTimer.Stop();

            base.OnNavigatedFrom(e);
        }
    }
}
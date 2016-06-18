using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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

        private readonly MapIcon currentLocationMapIcon;
        private readonly Geolocator geolocator;

        public EditPage()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();    //DispatcherTimer to periodically update the current time
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (sender, o) => { ViewModel.EditNote.LastModified = DateTime.Now; };   //Update the time in the ViewModel every 1 second

            currentLocationMapIcon = new MapIcon
            {
                Title = "You are here",
                NormalizedAnchorPoint = new Point(0.5, 0.5),
            };
            MapControl.MapElements.Add(currentLocationMapIcon);


            geolocator = new Geolocator();
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
                dispatcherTimer.Start();    //start the dispatcherTimer
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //when the user navigates away from this page
            ((App)Application.Current).OnBackRequested -= OnBackRequested;

            dispatcherTimer.Stop();  //stop the dispatcherTimer

            base.OnNavigatedFrom(e);
        }

        private async void CenterToCurrentLocation()
        {
            var access = await Geolocator.RequestAccessAsync();

            if (access == GeolocationAccessStatus.Allowed)
            {
                var geoposition = await geolocator.GetGeopositionAsync();
                var geopoint = geoposition.Coordinate.Point;

                currentLocationMapIcon.Location = geopoint;

                MapControl.Center = geopoint;
                MapControl.ZoomLevel = 15;
            }
        }

        private void ZoomIn()
        {
            if (MapControl.ZoomLevel < MapControl.MaxZoomLevel)
                MapControl.ZoomLevel++;
        }

        private void ZoomOut()
        {
            if (MapControl.ZoomLevel > MapControl.MinZoomLevel)
                MapControl.ZoomLevel--;
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using GalaSoft.MvvmLight.Messaging;
using NotesPCL.Models;

namespace Notes.Controls
{
    public sealed partial class NotesMap : UserControl
    {

        private Geopoint currentLocation;
        private readonly MapIcon currentLocationMapIcon;
        private readonly Geolocator geolocator;

        public NotesMap()
        {
            this.InitializeComponent();

            geolocator = new Geolocator();

            currentLocationMapIcon = new MapIcon
            {
                Title = "You are here",
                Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/Maps_current_location.png")),
                NormalizedAnchorPoint = new Point(0.5, 0.5),
            };

            Loaded += async (sender, args) =>
            {
                Messenger.Default.Register<string>(this, RecieveMessage);

                await DrawCurrentLocationIcon();
            };

            Unloaded += (sender, args) =>
            {
                Messenger.Default.Unregister<string>(this, RecieveMessage);
            };
        }

        public Geopoint MapCenter
        {
            get { return (Geopoint)GetValue(MapCenterProperty); }
            set { SetValue(MapCenterProperty, value); }
        }

        public double MapZoomLevel
        {
            get { return (double)GetValue(MapZoomLevelProperty); }
            set { SetValue(MapZoomLevelProperty, value); }
        }

        public ObservableCollection<Note> Notes
        {
            get { return (ObservableCollection<Note>)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        public static readonly DependencyProperty MapCenterProperty =
            DependencyProperty.Register("MapCenter", typeof(Geopoint), typeof(NotesMap), new PropertyMetadata(default(Geopoint)));

        public static readonly DependencyProperty MapZoomLevelProperty =
            DependencyProperty.Register("MapZoomLevel", typeof(double), typeof(NotesMap), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty NotesProperty =
            DependencyProperty.Register("Notes", typeof(ObservableCollection<Note>), typeof(NotesMap), new PropertyMetadata(default(ObservableCollection<Note>)));

        public event EventHandler<Note> MapPinClicked;

        private void RecieveMessage(string message)
        {
            if (message == "zoomToFit")
                ZoomToFitAllNotePins();

            else if (message == "centerToCurrentLocation")
                CenterToCurrentLocation();

        }

        private async Task DrawCurrentLocationIcon()
        {
            var access = await Geolocator.RequestAccessAsync();

            if (access == GeolocationAccessStatus.Allowed)
            {
                var geoposition = await geolocator.GetGeopositionAsync();
                currentLocation = geoposition.Coordinate.Point;

                currentLocationMapIcon.Location = currentLocation;

                if (!MapControl.MapElements.Contains(currentLocationMapIcon))
                    MapControl.MapElements.Add(currentLocationMapIcon);
            }
            else
            {
                currentLocation = null;
            }
        }

        private async void CenterToCurrentLocation()
        {
            await DrawCurrentLocationIcon();

            if (currentLocation == null)
                return;

            MapCenter = currentLocation;
            await MapControl.TryZoomToAsync(13);
        }

        private async void ZoomIn()
        {
            await MapControl.TryZoomInAsync();
        }

        private async void ZoomOut()
        {
            await MapControl.TryZoomOutAsync();
        }

        private async void ZoomToFitAllNotePins()
        {
            if (Notes == null)
                return;

            var basicGeopositions = Notes.Where(note => note.CreationLocation.IsValid)
                                                        .Select(note => new BasicGeoposition
                                                        {
                                                            Latitude = note.CreationLocation.Latitude,
                                                            Longitude = note.CreationLocation.Longitude,
                                                        });

            var box = GeoboundingBox.TryCompute(basicGeopositions);

            await MapControl.TrySetViewBoundsAsync(box, new Thickness(70), MapAnimationKind.None);
        }

        //Handles clicks on note pins on the map
        private void UIElement_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var stackPanel = sender as StackPanel;

            var note = stackPanel?.DataContext as Note;

            if (note != null)
                MapPinClicked?.Invoke(this, note);  //Forward clicked note to anyone who registered for the event
        }
    }
}

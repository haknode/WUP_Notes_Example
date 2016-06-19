﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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

            Messenger.Default.Register<string>(this, ZoomToFitAllNotePins);

            Loaded += (sender, args) =>
            {
                DrawCurrentLocationIcon();
            };
        }

        public Geopoint Center
        {
            get { return (Geopoint)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        public double ZoomLevel
        {
            get { return (double)GetValue(ZoomLevelProperty); }
            set { SetValue(ZoomLevelProperty, value); }
        }

        public ObservableCollection<Note> Notes
        {
            get { return (ObservableCollection<Note>)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        public static readonly DependencyProperty CenterProperty = 
            DependencyProperty.Register("Center", typeof(Geopoint), typeof(NotesMap), new PropertyMetadata(default(Geopoint)));

        public static readonly DependencyProperty ZoomLevelProperty = 
            DependencyProperty.Register("ZoomLevel", typeof(double), typeof(NotesMap), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty NotesProperty = 
            DependencyProperty.Register("Notes", typeof(ObservableCollection<Note>), typeof(NotesMap), new PropertyMetadata(default(ObservableCollection<Note>)));

        private async void DrawCurrentLocationIcon()
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
            if (currentLocation == null)
                return;

            Center = currentLocation;
            await MapControl.TryZoomToAsync(15);
        }

        private async void ZoomIn()
        {
            await MapControl.TryZoomInAsync();
        }

        private async void ZoomOut()
        {
            await MapControl.TryZoomOutAsync();
        }

        private async void ZoomToFitAllNotePins(string message)
        {
            if (message != "zoomToFit")
                return;

            var basicGeopositions = Notes.Where(note => note.CreationLocation.IsValid)
                                                        .Select(note => new BasicGeoposition
                                                        {
                                                            Latitude = note.CreationLocation.Latitude,
                                                            Longitude = note.CreationLocation.Longitude,
                                                        });

            var box = GeoboundingBox.TryCompute(basicGeopositions);

            await MapControl.TrySetViewBoundsAsync(box, new Thickness(100), MapAnimationKind.Linear);
        }
    }
}

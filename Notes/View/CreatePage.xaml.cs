using System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NotesPCL.ViewModel;

namespace Notes.View
{
    public sealed partial class CreatePage : Page
    {
        private readonly DispatcherTimer dispatcherTimer;

        public CreatePage()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();    //DispatcherTimer to periodically update the current time
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += (sender, o) => { ViewModel.Now = DateTime.Now; };   //Update the time in the ViewModel every 1 second
        }

        public CreateViewModel ViewModel => (CreateViewModel) DataContext;

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
            ((App) Application.Current).OnBackRequested += OnBackRequested;

            dispatcherTimer.Start();    //start the dispatcherTimer
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //when the user navigates away from this page
            ((App) Application.Current).OnBackRequested -= OnBackRequested;

            dispatcherTimer.Stop();  //stop the dispatcherTimer
        }
    }
}
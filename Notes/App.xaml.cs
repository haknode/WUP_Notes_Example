using System;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.ApplicationInsights;
using Notes.View;

namespace Notes
{
    sealed partial class App : Application
    {
        public App()
        {
            WindowsAppInitializer.InitializeAsync(
                WindowsCollectors.Metadata |
                WindowsCollectors.Session);

            InitializeComponent();

            Suspending += (sender, e) =>
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                //TODO: Anwendungszustand speichern und alle Hintergrundaktivitäten beenden
                deferral.Complete();
            };
        }

        //This event signals that OnBack was requested
        public event EventHandler<BackRequestedEventArgs> OnBackRequested;

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed +=
                    (sender, args) => { throw new Exception("Failed to load Page " + args.SourcePageType.FullName); };

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Zustand von zuvor angehaltener Anwendung laden
                }

                Window.Current.Content = rootFrame;

                //Handle the BackRequested Event
                SystemNavigationManager.GetForCurrentView().BackRequested += (sender, eventArgs) =>
                {
                    //If an EventHandler is registered for the OnBackRequested event: call it
                    //Maybe the EventHandler will handled this event
                    OnBackRequested?.Invoke(this, eventArgs);

                    if (!eventArgs.Handled)
                    {
                        var frame = Window.Current.Content as Frame;
                        if (frame != null && frame.CanGoBack)   //If we can go back, go back
                        {
                            eventArgs.Handled = true;
                            frame.GoBack();
                        }
                    }
                };

                //Whenever we navigate to a new page, set the AppViewBackButton to visible if we can go back, to collapsed if we dont
                rootFrame.Navigated += (sender, args) =>
                {
                    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                        = rootFrame.CanGoBack
                            ? AppViewBackButtonVisibility.Visible
                            : AppViewBackButtonVisibility.Collapsed;
                };
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(StartPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }
    }
}
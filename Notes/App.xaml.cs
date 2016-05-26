using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Notes.View;
using NotesPCL.ViewModel;

namespace Notes
{
    sealed partial class App : Application
    {
        //This event signals that OnBack was requested
        public event EventHandler<BackRequestedEventArgs> OnBackRequested;

        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            this.InitializeComponent();

            this.Suspending += (sender, e) =>
            {
                var deferral = e.SuspendingOperation.GetDeferral();
                //TODO: Anwendungszustand speichern und alle Hintergrundaktivitäten beenden
                deferral.Complete();
            };


        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += (sender, args) =>
                {
                    throw new Exception("Failed to load Page " + args.SourcePageType.FullName);
                };

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Zustand von zuvor angehaltener Anwendung laden
                }

                Window.Current.Content = rootFrame;

                SystemNavigationManager.GetForCurrentView().BackRequested += (sender, eventArgs) =>
                {
                    OnBackRequested?.Invoke(this, eventArgs);

                    if (!eventArgs.Handled)
                    {
                        Frame frame = Window.Current.Content as Frame;
                        if (frame != null && frame.CanGoBack)
                        {
                            eventArgs.Handled = true;
                            frame.GoBack();
                        }
                    }
                };

                rootFrame.Navigated += (sender, args) =>
                    {
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility
                            = rootFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
                    };

            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(View.StartPage    ), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

    }
}

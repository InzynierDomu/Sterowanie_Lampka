using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Media.SpeechRecognition;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Storage;
using Windows.ApplicationModel.Core;

using System.Diagnostics;
using Windows.UI.Popups;

namespace test_app_for_cortana
{
    sealed partial class App : Application
    {
        private Mqtt_commands test = new Mqtt_commands();

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                }

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();

                try
                {
                    StorageFile vcd = await Package.Current.InstalledLocation.GetFileAsync(@"test_app_for_cortana.xml");
                    await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(vcd);
                } catch(Exception ex)
                {
                    Debug.Write("Failed error: " + ex.Message);
                }
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            if (args.Kind == ActivationKind.VoiceCommand)
            {
                VoiceCommandActivatedEventArgs cmd = args as VoiceCommandActivatedEventArgs;
                SpeechRecognitionResult result = cmd.Result;

                string commandName = result.RulePath[0];

                MessageDialog dialog = new MessageDialog("");

                switch(commandName)
                {
                    case "TurnOnDeskLight":
                        test.turningOnLightMqtt();
                        break;
                    case "TurnOffDeskLight":
                        test.turningOffLightMqtt();
                        break;
                    default:
                        Debug.WriteLine("Couldn't find command name");
                        break;
                }               
            }
            CoreApplication.Exit();
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }


        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
    }
}

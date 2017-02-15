using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using ConferenceRoom.Common;
using Windows.Media.SpeechRecognition;
using System.Linq;
using Windows.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConferenceRoom
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        public static NavigationService NavigationService { get; private set; }
        private RootFrameNavigationHelper rootFrameNavigationHelper;

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            try
            {
             // Install the VCD. 
                StorageFile vcdStorageFile = await Package.Current.InstalledLocation.GetFileAsync(@"ConferenceRoomCommands.xml");
                await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);
            }
            catch (Exception ex)
            {
                //If you are not seeing your VCD in your personal Cortana-powered apps list, check to see if 
                //there is an error.
                Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
            }

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                App.NavigationService = new NavigationService(rootFrame);
                rootFrameNavigationHelper = new RootFrameNavigationHelper(rootFrame);

                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
                rootFrame.Navigate(typeof(View.RaleighDemo), "");

            Window.Current.Activate();

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


        protected async override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            Type navigationToPageType;

            if (args.Kind == ActivationKind.VoiceCommand)
            {
                var commandArgs = args as VoiceCommandActivatedEventArgs;

                SpeechRecognitionResult speechRecognitionResult = commandArgs.Result;
                string voiceCommandName = speechRecognitionResult.RulePath[0];

                switch (voiceCommandName)
                {
                    case "starWarsIntro":
                        navigationToPageType = typeof(View.SWIntro);
                        break;
                    case "imperialMarch":
                        navigationToPageType = typeof(View.Imperial);
                        break;
                    case "cf_FaceBook":
                        await CFTWT();
                        navigationToPageType = null;
                        break;
                    case "cf_Twitter":
                        await CFFB();
                        navigationToPageType = null;
                        break;
                    case "openPPTpres":
                        await PPoint();
                        navigationToPageType = null;
                        break;
                    case "redAlert":
                            navigationToPageType = typeof(View.RedAlert);
                        break;
                    case "releaseNight":
                        navigationToPageType = typeof(View.ReleaseNight);
                        break;
                    default:
                        navigationToPageType = typeof(View.RaleighDemo);
                        break;
                }
            }
            else if (args.Kind == ActivationKind.Protocol)
            {
                var commandArgs = args as ProtocolActivatedEventArgs;
                Windows.Foundation.WwwFormUrlDecoder decoder = new Windows.Foundation.WwwFormUrlDecoder(commandArgs.Uri.Query);
                var destination = decoder.GetFirstValueByName("LaunchContext");

                navigationToPageType = typeof(View.RaleighDemo);
            }
            else
            {
                navigationToPageType = typeof(View.RaleighDemo);
            }

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                NavigationService = new NavigationService(rootFrame);

                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }


            if (navigationToPageType != null)
            {
                rootFrame.Navigate(navigationToPageType, null);
            }
            Window.Current.Activate();

        }

        private async Task CFTWT()
        {
            try
            {
                var uri = new Uri(@"https://twitter.com/CoderFoundry?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor");
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Handling Voice Command failed " + ex.ToString());
            }
        }

        private async Task CFFB()
        {
            try
            {
                var uri = new Uri(@"https://www.facebook.com/coderfoundry/");
                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Handling Voice Command failed " + ex.ToString());
            }
        }

        private async Task PPoint()
        {

            try
            {
                //Path to the file in the app package to launch
                string imageFile = @"Assets\coderfoundry.ppsx";

                var file = await Package.Current.InstalledLocation.GetFileAsync(imageFile);

                if (file != null)
                {
                    // Launch the retrieved file
                    var success = await Windows.System.Launcher.LaunchFileAsync(file);

                    if (success)
                    {
                        // File launched
                    }
                    else
                    {
                        // File launch failed
                    }
                }
                else
                {
                    // Could not find file
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error opening file  " + ex.ToString());
            }
        }

        private string SemanticInterpretation(string interpretationKey, SpeechRecognitionResult speechRecognitionResult)
        {
            return speechRecognitionResult.SemanticInterpretation.Properties[interpretationKey].FirstOrDefault();
        }
    }
}

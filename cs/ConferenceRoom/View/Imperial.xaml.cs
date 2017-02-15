using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ConferenceRoom.Common;
using Q42.HueApi;
using ConferenceRoom.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConferenceRoom.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Imperial : Page
    {
        private readonly NavigationHelper _navigationHelper;
        private readonly HueClient _hueClient;
        private MediaElement element;
        //private BridgeInfo _hueBridge;

        public Imperial()
        {
            InitializeComponent();

            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += NavigationHelperOnLoadState;
            //_navigationHelper.SaveState += (sender, args) => { MediaSimple.MediaPlayer.Pause(); };

            //Task.Run(() => this.GetHueInfo()).Wait();
            //_hueClient = new LocalHueClient(_hueBridge.internalipaddress);
            //_hueClient.Initialize("");

            BitmapImage bitmapImage = new BitmapImage();
            // Call BaseUri on the root Page element and combine it with a relative path
            // to consruct an absolute URI.
            bitmapImage.UriSource = new Uri(BaseUri, "/Assets/vadar2.jpg");
            IMImage.Source = bitmapImage;


            _hueClient = new LocalHueClient("10.10.10.193");
            _hueClient.Initialize("CqnjoQvPHKdy1IkFVJggaTdflgWBF1HWQTOZYf2p");
        }

        //private async Task GetHueInfo()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        try
        //        {
        //            var json = await client.GetStringAsync(new Uri("https://www.meethue.com/api/nupnp"));

        //            var result = JsonConvert.DeserializeObject<List<BridgeInfo>>(json);

        //            _hueBridge = result.FirstOrDefault();
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("Error obtaining Hue Bridge information: " + ex.ToString());
        //        }
        //    }
        //}

        private async void NavigationHelperOnLoadState(object sender, LoadStateEventArgs e)
        {
            //var uri = new Uri("ms-appx:///Videos/ReleaseNight.wmv");
            //MediaSimple.AutoPlay = true;
            //MediaSimple.IsFullWindow = true;
            //var src = MediaSource.CreateFromUri(uri);
            //MediaSimple.Source = src;

            element = new MediaElement();
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
            var file = await folder.GetFileAsync("imperial_march.wav");
            var stream = await file.OpenAsync(FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();

            var scenario = new Scenarios.ReleaseNightScenario(_hueClient);
            scenario.ExecuteAsync(CancellationToken.None);
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            element.Stop();
            _navigationHelper.OnNavigatedFrom(e);
        }
    }
}

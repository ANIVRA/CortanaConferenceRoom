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
using Windows.ApplicationModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConferenceRoom.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PowerPoint : Page
    {
        private IReadOnlyCollection<Windows.Storage.StorageFile> _imgFiles;
        private readonly NavigationHelper _navigationHelper;
        public List<string> pptImages;
        public List<string> PPTList;

        public PowerPoint()
        {
            InitializeComponent();

            _navigationHelper = new NavigationHelper(this);
            _navigationHelper.LoadState += NavigationHelperOnLoadState;

            //foreach (var filePath in _imgFiles)
            //{
            //    pptImages.Add(filePath.Path);
            //}

            flipView.ItemsSource = pptImages;

        }

        public async Task GetImages()
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");

            _imgFiles = await folder.GetFilesAsync();
        }


        private void NavigationHelperOnLoadState(object sender, LoadStateEventArgs e)
        {
            //var uri = new Uri("ms-appx:///Videos/RedAlert.wmv");
            //MediaSimple.AutoPlay = true;
            //MediaSimple.IsFullWindow = true;
            //var src = MediaSource.CreateFromUri(uri);
            //MediaSimple.Source = src;

            //var scenario = new Scenarios.RedAlertScenario(_hueClient);
            //scenario.ExecuteAsync(CancellationToken.None);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }
    }
}

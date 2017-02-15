using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ConferenceRoom.Common;
using ConferenceRoom.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConferenceRoom.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RaleighDemo : Page
    {
        public RaleighDemo()
        {
            InitializeComponent();

            var navigationHelper = new NavigationHelper(this);
            navigationHelper.LoadState += NavigationHelperOnLoadState;
            //navigationHelper.SaveState += (sender, args) => {};
        }

        public RaleighDemoViewModel ViewModel { get; private set; }

        private void NavigationHelperOnLoadState(object sender, LoadStateEventArgs loadStateEventArgs)
        {
            ViewModel = (RaleighDemoViewModel) DataContext;
        }
    }
}

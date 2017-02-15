using System.Windows.Input;
using ConferenceRoom.Common;

namespace ConferenceRoom.ViewModel
{
    public class RaleighDemoViewModel : ViewModelBase
    {
        public RaleighDemoViewModel()
        {
            OpenRedAlert = new RelayCommand(() => App.NavigationService.Navigate<View.RedAlert>());
            //OpenPowerPoint = new RelayCommand(() => App.NavigationService.Navigate<View.PowerPoint>());

        }

        public ICommand OpenRedAlert { get; private set; }
        //public ICommand OpenPowerPoint { get; private set; }
    }
}

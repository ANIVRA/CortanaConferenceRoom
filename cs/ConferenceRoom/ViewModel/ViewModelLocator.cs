using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceRoom.ViewModel
{
    public class ViewModelLocator
    {
        private Dictionary<string, ViewModelBase> modelSet = new Dictionary<string, ViewModelBase>();

        public ViewModelLocator()
        {
            modelSet.Add("RaleighDemoViewModel", new RaleighDemoViewModel());
        }

        public RaleighDemoViewModel RaleighDemoViewModel => (RaleighDemoViewModel)modelSet["RaleighDemoViewModel"];

    }
}

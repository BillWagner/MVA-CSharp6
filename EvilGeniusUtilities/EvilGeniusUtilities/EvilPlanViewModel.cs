using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilGeniusUtilities
{
    public class EvilPlanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string EvilPlanName {
            get { return evilPlanName;  }
            set { evilPlanName = value; }
        }
        private string evilPlanName;
    }
}

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
            set {
                if (evilPlanName != value)
                {
                    var handler = PropertyChanged;
                    if (handler != null)
                        handler(this, new PropertyChangedEventArgs("EvilPlanName"));
                    evilPlanName = value;
                }
            }
        }
        private string evilPlanName;
    }
}

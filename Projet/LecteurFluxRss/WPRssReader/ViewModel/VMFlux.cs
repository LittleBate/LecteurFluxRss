using Buisness;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPRssReader.ViewModel
{
    public class VMFlux : INotifyPropertyChanged
    {
        public VMFlux()
        {
            
        }

        private const string CURRENT_FLUX = "CurrentFlux";
        private Flux currentFlux;
        public Flux CurrentFlux 
        {
            get
            {
                return currentFlux;
            }
            set
            {
                if (value == currentFlux)
                    return;
                currentFlux = value;
                OnPropertyChanged(CURRENT_FLUX);
            }
        }

        public VMApplication VMApplication
        {
            get
            {
                return VMApplication.Instance;
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}

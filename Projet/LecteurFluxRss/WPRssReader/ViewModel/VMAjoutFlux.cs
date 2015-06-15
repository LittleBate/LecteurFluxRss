using Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPRssReader.Common;

namespace WPRssReader.ViewModel
{
    public class VMAjoutFlux : INotifyPropertyChanged
    {
        private RelayCommand cmdAjoutFlux;
        /// <summary>
        /// Obtient la commande qui permet l'ajout d'un nouveau flux
        /// </summary>
        public ICommand CmdAjoutFlux
        {
            get 
            { 
                if(cmdAjoutFlux == null)
                {
                    cmdAjoutFlux = new RelayCommand(() =>
                        {
                            AjoutFlux();
                        }
                    );
                }
                return cmdAjoutFlux; 
            }
        }

        /// <summary>
        /// Permet l'ajout d'un flux à partir du liens stocké
        /// </summary>
        public void AjoutFlux()
        {
            if(String.IsNullOrEmpty(Liens))
                return;
            FluxManager.getInstance().LoadALink("http://" + Liens);
            Liens = String.Empty;
        }

        private string LIENS = "Liens";
        private string liens;
        public string Liens
        {
            get
            {
                return liens;
            }
            set
            {
                liens = value;
                OnPropertyChanged(LIENS);
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

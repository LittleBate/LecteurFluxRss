using Buisness;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPRssReader.Common;

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

        private const string CURRENT_ARTICLE = "CurrentArticle";
        private Article currentArticle;
        public Article CurrentArticle
        {
            get
            {
                return currentArticle;
            }
            set
            {
                currentArticle = value;
                VMApplication.Instance.VMArticle.CurrentArticle = CurrentArticle;
                OnPropertyChanged(CURRENT_ARTICLE);
            }
        }

        public VMApplication VMApplication
        {
            get
            {
                return VMApplication.Instance;
            }
        }
            
        private RelayCommand cmdAddToFavori;
        public ICommand CmdAddToFavori
        {
            get
            {
                if(cmdAddToFavori == null)
                {
                }
                return cmdAddToFavori;
            }
        }

        private void AddToFavori(Flux flux)
        {
        }

        private RelayCommand cmdAddTag;
        public ICommand CmdAddTag
        {
            get
            {
                if(cmdAddTag == null)
                {
                    cmdAddTag = new RelayCommand(() =>
                    {

                    });
                }
                return cmdAddTag;
            }
        }

        private void AddTag(Flux flux)
        {

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

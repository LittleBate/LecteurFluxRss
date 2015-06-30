using Buisness;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WPRssReader.Common;
using WPRssReader.View;

namespace WPRssReader.ViewModel
{
    public class VMFlux 
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
                ((Frame)Window.Current.Content).Navigate(typeof(VFlux));
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
                Frame frame = (Frame)Window.Current.Content;
                if (frame != null && frame.CurrentSourcePageType == typeof(VFlux))
                    frame.Navigate(typeof(PivotArticle));
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
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness;
using System.Collections.ObjectModel;
using DataWP;
using Windows.UI.Xaml.Controls;

namespace WPRssReader.ViewModel
{
    public class VMApplication
    {
        /// <summary>
        /// Instance unique du ViewModel de l'application
        /// </summary>
        private static VMApplication instance;

        /// <summary>
        /// Obtient l'instance unique du ViewModel de l'application
        /// </summary>
        public static VMApplication Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new VMApplication();
                }
                return instance;
            }
        }
        
        private VMApplication()
        {
            manager = FluxManager.getInstance();
            manager.SauvegardeManager = new SauvegardeManagerXml();
            manager.loadLinks();

            VMFlux = new VMFlux();
            VMArticle = new VMArticle();
            VMAjoutFlux = new VMAjoutFlux();
        }

        public ObservableCollection<Flux> ListeFlux
        {
            get
            {
                return manager.ListeFlux;
            }
        }

        private FluxManager manager;

        public VMFlux VMFlux { get; private set; }

        public VMArticle VMArticle { get; private set; }

        public VMAjoutFlux VMAjoutFlux { get; private set; }
    }
}

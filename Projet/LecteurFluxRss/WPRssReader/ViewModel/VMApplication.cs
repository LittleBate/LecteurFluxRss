using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness;
using System.Collections.ObjectModel;

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

            manager.AddFluxLink(@"http://www.developpez.com/index/rss");
            manager.AddFluxLink(@"http://radiofrance-podcast.net/podcast09/rss_13100.xml");
            manager.AddFluxLink(@"http://lesjoiesducode.fr/rss");

            manager.Load();

            VMFlux = new VMFlux();
            VMArticle = new VMArticle();
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
    }
}

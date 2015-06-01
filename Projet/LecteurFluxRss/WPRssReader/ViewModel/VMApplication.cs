using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness;

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
            //manager.SauvegardeManager = new SauvegardeManagerBdd();

            manager.AddFluxLink(@"http://www.developpez.com/index/rss");
            //manager.AddFluxTitle(@"http://radiofrance-podcast.net/podcast09/rss_13100.xml");
            //manager.AddFluxTitle(@"http://lesjoiesducode.fr/rss");
            manager.ChargerDonnées();
            manager.Load();
        }

        private FluxManager manager;
        

    }
}

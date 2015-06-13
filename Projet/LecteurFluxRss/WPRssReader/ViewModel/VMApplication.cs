using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness;
using System.Collections.ObjectModel;
using DataWP;

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
            

            manager.LoadALink(@"http://www.developpez.com/index/rss");
            manager.LoadALink(@"http://radiofrance-podcast.net/podcast09/rss_13100.xml");
            manager.LoadALink(@"http://lesjoiesducode.fr/rss");
            manager.LoadALink(@"http://www.lequipe.fr/rss/actu_rss.xml");
            manager.LoadALink(@"http://www.lequipe.fr/rss/videos_rss.xml");
            manager.LoadALink(@"http://liberation.fr.feedsportal.com/c/32268/fe.ed/rss.liberation.fr/rss/19/");

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

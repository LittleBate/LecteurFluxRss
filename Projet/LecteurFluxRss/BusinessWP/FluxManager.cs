using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    /// <summary>
    /// Manager de l'ensemble des flux rss de l'application
    /// </summary>
    public class FluxManager
    {
        /// <summary>
        /// Nom du flux des articles favoris
        /// </summary>
        public const string FAVORIS = "Favoris";

        #region singleton
        
        /// <summary>
        /// Instance unique de la classe
        /// </summary>
        private static FluxManager instance;

        /// <summary>
        /// Obtient l'instance unique de la classe
        /// </summary>
        /// <returns></returns>
        public static FluxManager getInstance()
        {
            if (instance == null)
                instance = new FluxManager();
            return instance;
        }

        private FluxManager()
        {
            ListeFlux = new ObservableCollection<Flux>();
            loader = LoaderFluxRSS.GetInstance();
            loader.FluxLoaded += loader_FluxLoaded;
        }

        #endregion

        #region Flux

        private ObservableCollection<Flux> listeFlux;
        /// <summary>
        /// liste des flux 
        /// </summary>
        public ObservableCollection<Flux> ListeFlux
        {
            get
            {
                return listeFlux;
            }
            private set
            {
                this.listeFlux = value;
            }
        }

        /// <summary>
        /// Ajoute un flux à la liste des flux stockés par le manager
        /// </summary>
        /// <param name="flux">le nouveau flux à ajouter</param>
        private void AddFlux(Flux flux)
        {
            listeFlux.Add(flux);
        }

        /// <summary>
        /// Ajoute un ensemble de flux à la liste des flux stockés par le manager
        /// </summary>
        /// <param name="listeFlux">liste de flux à ajouter au manager</param>
        private void AddAllFlux(IEnumerable<Flux> listeFlux)
        {
            foreach (var flux in listeFlux)
            {
                AddFlux(flux);
            }
        }

        /// <summary>
        /// Ajoute un article à la liste des article favoris de l'utilisateur
        /// </summary>
        /// <param name="article">article à ajouter</param>
        public void AddArticleFavori(Article article)
        {
            Flux favoris = ListeFlux.Where(f => f.Title.Trim().Equals(FAVORIS)).FirstOrDefault();
            if (favoris == null)
            {
                favoris = new Flux()
                {
                    Title = FAVORIS
                };
                AddFlux(favoris);
            }

            favoris.AddArticle(article);
            sauvegardeManager.AddFavoris(article);
        }

        #endregion

        #region Loader

        public void LoadALink(string link)
        {
            loader.Load(link);
        }

        void loader_FluxLoaded(object sender, FluxLoadedEventArgs e)
        {
            AddFlux(e.fluxLoaded);
        }
        
        /// <summary>
        /// Objet qui permet le chargement des flux rss sur internet
        /// </summary>
        private LoaderFluxRSS loader;

        private ISauvegardeManager sauvegardeManager;
        /// <summary>
        /// Definit l'objet responsable de la sauvegarde des données
        /// </summary>
        public ISauvegardeManager SauvegardeManager
        {
            set
            {
                sauvegardeManager = value;
            }
        }

        /// <summary>
        /// Chargement des données sauvegardées.
        /// </summary>
        public void ChargerDonnées()
        {
            if(sauvegardeManager == null)
            {
                return;
            }
            foreach (var link in sauvegardeManager.LoadLinks())
	        {
                LoadALink(link);
	        }
        }
        
        #endregion

        #region Tag

        /// <summary>
        /// Obtient la liste de l'ensemble des tags existant en mémoire.
        /// </summary>
        public IEnumerable<Tag> listeTagExistants
        {
            get
            {
                List<Tag> listeTag = new List<Tag>();

                foreach (var flux in listeFlux)
                {
                    listeTag.AddRange(flux.ListTag);
                }
                listeTag.AddRange(listeFlux.ToList().SelectMany(f => f.ListArticle).SelectMany(a => a.ListTag));
                
                return listeTag.Distinct();
            }
        }
        
        /// <summary>
        /// Obtient la liste des flux qui contiennent un tag
        /// </summary>
        /// <param name="_tag">le tag recherché</param>
        /// <returns>la liste des flux correspondants</returns>
        public List<Flux> getFluxByTag(Tag _tag)
        {
            return this.ListeFlux.Where(a => a.ListTag.Contains(_tag)).ToList();
        }

        /// <summary>
        /// Obtient la liste des articles qui contiennent un tag
        /// </summary>
        /// <param name="_tag">le tag recherché</param>
        /// <returns>la liste des articles correspondants</returns>
        public List<Article> getArticleByTag(Tag _tag)
        {
            return this.ListeFlux.SelectMany(F => F.ListArticle.Where(a => a.ListTag.Contains(_tag))).ToList();
        }

        #endregion


    }
}

﻿using System;
using System.Collections.Generic;
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
            ListeFlux = new List<Flux>();
            ListFluxLink = new List<string>();
            loader = LoaderFluxRSS.GetInstance();
        }

        #endregion

        #region Flux

        private List<Flux> listeFlux;
        /// <summary>
        /// liste des flux 
        /// </summary>
        public List<Flux> ListeFlux
        {
            get
            {
                return new List<Flux>(listeFlux);
            }
            private set
            {
                this.listeFlux = value;
            }
        }

        /// <summary>
        /// Liste des titre des flux rss à charger
        /// </summary>
        private List<String> ListFluxLink;

        /// <summary>
        /// Ajoute un nouveau lien de flux rss à charger
        /// </summary>
        /// <param name="link">lien à ajouter</param>
        public void AddFluxLink(string link)
        {
            ListFluxLink.Add(link);
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
            listeFlux.ToList().ForEach(f => AddFlux(f));
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

        /// <summary>
        /// Charge les flux Rss à partir d'internet
        /// </summary>
        public void Load()
        {

            ListFluxLink
                .Where(l => !l.Trim().Equals(FAVORIS))
                .ToList()
                .ForEach(l => AddFlux(loader.Load(l)));

            listeFlux.Where(f => String.IsNullOrEmpty(f.Title))
                .ToList()
                .ForEach(f => f.Title = f.Link);
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
            AddAllFlux(sauvegardeManager.Load());
            listeFlux.Where(f => String.IsNullOrEmpty(f.Title))
                .ToList()
                .ForEach(f => AddFluxLink(f.Link));
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

                listeFlux.ToList().ForEach(f => listeTag.AddRange(f.ListTag));
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

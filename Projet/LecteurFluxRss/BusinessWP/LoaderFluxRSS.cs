using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.Web.Http;

namespace Buisness
{
    public class LoaderFluxRSS
    {

        #region Const
        /// <summary>
        /// channel
        /// </summary>
        private const string CHANNEL = "channel";
        /// <summary>
        /// title
        /// </summary>
        private const string TITLE = "title";
        /// <summary>
        /// link
        /// </summary>
        private const string LINK = "link";
        /// <summary>
        /// description
        /// </summary>
        private const string DESCRIPTION = "description";
        /// <summary>
        /// item
        /// </summary>
        private const string ITEM = "item";
        /// <summary>
        /// pubDate
        /// </summary>
        private const string DATE = "pubDate";
        /// <summary>
        /// Fichier de stockage des flux rss le temps de leur lecture
        /// </summary>
        private const string STOCKFILEPATH = "stockFile.xml";
        /// <summary>
        /// Fichier d'erreur
        /// </summary>
        private const string LOGERRORFILE = "LogError.txt";
        /// <summary>
        /// Auteur
        /// </summary>
        private const string AUTHOR = "author";
        /// <summary>
        /// Media
        /// </summary>
        private const string MEDIA = "enclosure";
        /// <summary>
        /// URL
        /// </summary>
        private const string URL = "url";

        #endregion

        #region Singleton

        /// <summary>
        /// Instance unique de chargeur de flux rss
        /// </summary>
        private static LoaderFluxRSS instance;

        private LoaderFluxRSS()
        {
            //wc = new WebClient();
            hc = new HttpClient();
        }

        /// <summary>
        /// Obtient l'instance unique de la classe
        /// </summary>
        /// <returns></returns>
        public static LoaderFluxRSS GetInstance()
        {
            if (instance == null)
            {
                instance = new LoaderFluxRSS();
            }
            return instance;
        }

        #endregion

        #region LoadFlux

        /// <summary>
        /// Connection permettant la lecture des flux rss via le web
        /// </summary>
        //private WebClient wc;

        private HttpClient hc;

        /// <summary>
        /// Charge un flux rss à partir du lien donné en paramètre
        /// </summary>
        /// <param name="FluxRssLink">lien du flux rss à charger</param>
        /// <returns>Le flux rss chargé</returns>
        public async void Load(string FluxRssLink)
        {
            var response = await hc.GetStringAsync(new Uri(FluxRssLink, UriKind.Absolute));
            var flux = LoadFluxByText(response);
            OnFluxLoaded(new FluxLoadedEventArgs(flux));
        }

        public event EventHandler<FluxLoadedEventArgs> FluxLoaded;
        public void OnFluxLoaded(FluxLoadedEventArgs args)
        {
            if(FluxLoaded != null)
            {
                FluxLoaded(this, args);
            }
        }

        private Flux LoadFluxByText(string text)
        {
            XDocument xDoc = XDocument.Parse(text);
            return LoadFlux(xDoc);
        }

        /// <summary>
        /// Charge le flux rss enregistrédans le fichier donné en paramètre
        /// </summary>
        /// <param name="fluxPath">Chein du fichier à charger</param>
        /// <returns>le flux rss chargé</returns>
        public Flux loadByFile(string fluxPath)
        {
            XDocument xDoc = null;
            try
            {
                xDoc = XDocument.Load(fluxPath);
            }
            catch(Exception e)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\n\n***********************************\n\n");
                sb.Append(e.Message).Append("\n\n").Append(e.StackTrace).Append("\n\n")
                    //.Append(e.TargetSite).Append("\n\n").Append(e.InnerException)
                    .Append("\n\n").Append(e.HelpLink).Append("\n\n");
                WriteError("Chargement des flux Rss (LoaderFluxRSS : public List<Flux> LoadFlux(List<String> _names))", sb.ToString());
            }
            if (xDoc == null)
                return null;
            return LoadFlux(xDoc);
        }
        
        /// <summary>
        /// Chargement d'un flux rss à partir d'un XDocument
        /// </summary>
        /// <param name="xDoc">Le xDoc contenant le flux</param>
        /// <returns>le flux rss chargé</returns>
        private Flux LoadFlux(XDocument xDoc)
        {
            XElement xFlux = xDoc.Descendants(CHANNEL).First();
            Flux flux = new Flux()
            {
                Title = xFlux.Element(TITLE).Value,
                Link = xFlux.Element(LINK).Value,
                Description = xFlux.Element(DESCRIPTION).Value
            };
            foreach (var xItem in xFlux.Elements(ITEM).ToList())
            {
                var article = new Article()
                {
                    Title = xItem.Element(TITLE).Value,
                    Link = xItem.Element(LINK).Value,
                    Description = xItem.Element(DESCRIPTION).Value
                };
                if (xItem.Element(MEDIA) != null)
                {
                    article.IconUrl = xItem.Element(MEDIA).Attribute(URL).Value;
                }
                if (xItem.Element(DATE) != null)
                {
                    DateTime date;
                    DateTime.TryParse(xItem.Element(DATE).Value, out date);
                    article.DatePublication = date;
                }
                flux.AddArticle(article);
            }
            return flux;
        }

        /// <summary>
        /// Charge l'ensemble des flux rss correspondant à la liste de liens donnés.
        /// </summary>
        /// <param name="links">liste des liens de flux rss à charger</param>
        /// <returns>la liste des flux rss chargé</returns>
        public void LoadFlux(List<String> links)
        {
            foreach (var link in links)
            {
                Load(link);
            }
        }

        #endregion

        #region Error

        /// <summary>
        /// Ecrit un message dans le fichier de log.
        /// </summary>
        /// <param name="source">source de l'erreur</param>
        /// <param name="message">message à transmettre</param>
        public static void WriteError(string source, string message)
        {

//            StringBuilder sb = new StringBuilder();
//            sb.Append("Erreur (").Append(DateTime.Now).Append(") : \n");
//            sb.Append("ou : ").Append(source).Append("\n");
//            sb.Append("pourquoi: ").Append(message).Append("\n");
//
//            using (TextWriter tw = new StreamWriter(LOGERRORFILE))
//            {
//                tw.Write(sb.ToString());
//            }
        }

        #endregion


    }

    public class FluxLoadedEventArgs
    {
        public FluxLoadedEventArgs(Flux flux)
        {
            fluxLoaded = flux;
        }

        public Flux fluxLoaded { get; private set; }
    }
}

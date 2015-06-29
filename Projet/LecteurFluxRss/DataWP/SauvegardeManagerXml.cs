using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness;
using System.Xml.Linq;
using System.Xml;
using Windows.Storage;
using System.IO;

namespace DataWP
{
    public class SauvegardeManagerXml : ISauvegardeManager
    {
        private XDocument xDoc;

        public SauvegardeManagerXml()
        {
            xDoc = new XDocument();
            //InitFile();
        }

        private async void InitFile()
        {
            xDoc = new XDocument();
            XElement xe = new XElement(LINKS_LIST,
                new XElement(LINK, @"http://www.developpez.com/index/rss"),
                new XElement(LINK, @"http://radiofrance-podcast.net/podcast09/rss_13100.xml"),
                new XElement(LINK, @"http://lesjoiesducode.fr/rss"),
                new XElement(LINK, @"http://www.lequipe.fr/rss/actu_rss.xml"),
                new XElement(LINK, @"http://www.futura-sciences.com/rss/actualites.xml"),
                new XElement(LINK, @"http://rss.nouvelobs.com/c/32581/fe.ed/www.sciencesetavenir.fr/rss.xml"),
                new XElement(LINK, @"http://www.science-et-vie.com/feed/"),
                new XElement(LINK, @"http://www.scienceshumaines.com/rss/"),
                new XElement(LINK, @"http://www.bonjourmadame.fr/rss"),
                new XElement(LINK, @"http://ux-fr.com/feed/"),
                new XElement(LINK, @"http://liberation.fr.feedsportal.com/c/32268/fe.ed/rss.liberation.fr/rss/19/")
                );

            xDoc.Add(xe);
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting);
            using (Stream s = await file.OpenStreamForWriteAsync())
            {
                xDoc.Save(s);
            }
        }

        #region CONST

        private const string FILE_NAME = "rssLinks.xml";
        private const string LINKS_LIST = "LinksList";
        private const string LINK = "Link";

        #endregion
        
        #region ISauvegardeManager Members

        public IEnumerable<Flux> Load()
        {
            return null;
        }

        public void Partager(Article art, IUser user)
        {
            throw new NotImplementedException();
        }

        public void AddFavoris(Article art)
        {
            throw new NotImplementedException();
        }

        public void AddAlirePlustard(Article art)
        {
            throw new NotImplementedException();
        }

        public async void SaveRssLink(string link)
        {
            xDoc.Element(LINKS_LIST).Add(new XElement(LINK, link));

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting);
            using (Stream s = await file.OpenStreamForWriteAsync())
            {
                xDoc.Save(s);
            }
        }

        /// <summary>
        /// Liste des liens de flux rss de l'utilisateur
        /// </summary>
        private List<string> listFlux;

        /// <summary>
        /// Vérifie l'existance d'un fichier dans le storagefolder donné
        /// </summary>
        /// <param name="fileName">nom du fichier cherché</param>
        /// <param name="folder">dossier contenant le fichier</param>
        /// <returns>retourne true si le fichier existe, false sinon</returns>
        private async Task<bool> FileExists(string fileName, StorageFolder folder)
        {
            try
            {
                StorageFile file = await folder.GetFileAsync(fileName); 
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async void LoadListLinks()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;

            if(FileExists(FILE_NAME, folder).Result)
            {
                using (Stream s = await folder.OpenStreamForReadAsync(FILE_NAME))
                {
                    xDoc = XDocument.Load(s);
                }
            }
            else
            {
                InitFile();
            }

            listFlux = new List<string>();

            foreach(var xLink in xDoc.Descendants(LINK))
            {
                listFlux.Add(xLink.Value);
            }
            OnListLinksLoaded();
        }

        public event EventHandler<EventArgs> ListLinksLoaded;
        private void OnListLinksLoaded()
        {
            if(ListLinksLoaded != null)
            {
                ListLinksLoaded.Invoke(this, null);
            }
        }

        IEnumerable<string> ISauvegardeManager.GetLinks()
        {
            return listFlux;
        }

        #endregion
    }
}

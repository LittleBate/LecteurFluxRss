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
            //SaveRssLink("");
        }

        #region CONST

        private const string FILE_NAME = "rssLink.xml";
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

//            xDoc = new XDocument();
//            XElement xe = new XElement(LINKS_LIST, 
//                new XElement(LINK, @"http://www.developpez.com/index/rss"),
//                new XElement(LINK, @"http://radiofrance-podcast.net/podcast09/rss_13100.xml"),
//                new XElement(LINK, @"http://lesjoiesducode.fr/rss"),
//                new XElement(LINK, @"http://www.lequipe.fr/rss/actu_rss.xml"),
//                new XElement(LINK, @"http://www.lequipe.fr/rss/videos_rss.xml"),
//                new XElement(LINK, @"http://liberation.fr.feedsportal.com/c/32268/fe.ed/rss.liberation.fr/rss/19/")
//                );
// 
//          //        new XElement(LINK, @"http: www.bonjourmadame/rss"),
             //xDoc.Add(xe);

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.ReplaceExisting);
            using (Stream s = await file.OpenStreamForWriteAsync())
            {
                xDoc.Save(s);
            }
        }

        private List<string> listFlux;

        public async void LoadListLinks()
        {
           
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            using (Stream s = await folder.OpenStreamForReadAsync(FILE_NAME))
            {
                xDoc = XDocument.Load(s);
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

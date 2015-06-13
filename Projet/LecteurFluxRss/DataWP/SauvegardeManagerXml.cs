using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buisness;
using System.Xml.Linq;
using System.Xml;
//using System.IO.
using System.IO;

namespace DataWP
{
    public class SauvegardeManagerXml : ISauvegardeManager
    {

        public SauvegardeManagerXml()
        {
            

            XDocument xDoc = new XDocument();

            var xLinkList = new XElement(LINKS_LIST,
                                new XElement(LINK, @"http://www.developpez.com/index/rss"),
                                new XElement(LINK, @"http://radiofrance-podcast.net/podcast09/rss_13100.xml"),
                                new XElement(LINK, @"http://lesjoiesducode.fr/rss"),
                                new XElement(LINK, @"http://www.lequipe.fr/rss/actu_rss.xml"),
                                new XElement(LINK, @"http://www.lequipe.fr/rss/videos_rss.xml"),
                                new XElement(LINK, @"http://liberation.fr.feedsportal.com/c/32268/fe.ed/rss.liberation.fr/rss/19/"),
                                new XElement(LINK, @"http://www.bonjourmadame.com/rss"));

            xDoc.Add(xLinkList);


            
        }

        private XDocument xDoc;

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

        public void SaveRssLink(string link)
        {

        }

        #endregion

        #region ISauvegardeManager Members


        public IEnumerable<string> LoadLinks()
        {
            List<string> listFlux = new List<string>();

            XDocument xDoc = XDocument.Load(FILE_NAME);

            foreach(var xLink in xDoc.Elements(LINK))
            {
                listFlux.Add(xLink.Value);
            }

            return listFlux;
        }

        #endregion
    }
}

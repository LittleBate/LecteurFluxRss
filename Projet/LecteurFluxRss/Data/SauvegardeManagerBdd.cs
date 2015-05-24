using Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;

namespace Data
{
    /// <summary>
    /// Manager des sauvegarde en Base de donnée
    /// </summary>
    public class SauvegardeManagerBdd : ISauvegardeManager
    {
        /// <summary>
        /// Le Context de donnée correspondant à notre basede donnée.
        /// utilisé pour faire du Linq to SQL
        /// </summary>
        private DataContext datacontext;

        /// <summary>
        /// Constructeur par defaut.
        ///Effectue la récupération du context de donnée
        /// </summary>
        public SauvegardeManagerBdd()
        {
            this.datacontext = 
                new DataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=U:\Licence\C#\FluxRssBd.mdf;");
        }

        /// <summary>
        /// Charge l'ensemble des flux enregistré en base
        /// </summary>
        /// <returns>liste de flux chargé</returns>
        public IEnumerable<Flux> Load()
        {
            var articles = from article in datacontext.GetTable<ArticleData>() select article;

            var temp = articles.ToList();
    
            var listeflux = from flux in datacontext.GetTable<FluxData>() select flux;

            listeflux.ToList()
                .ForEach(f => f.getFlux().AddAllArtciel(
                    temp.Where(a => a.FluxID == f.FluxID)
                    .Select(a => a.article).ToList()));
            


            List<Flux> lf = listeflux.Select(f => f.getFlux()).ToList();
            
            return lf;
        }

        /// <summary>
        /// Enregistre en base le partage d'un article
        /// </summary>
        /// <param name="art">article à partager</param>
        /// <param name="user">utilisateur destinataire</param>
        public void Partager(Article art, IUser user)
        {
            datacontext.GetTable<ArticleData>().InsertOnSubmit(new ArticleData(art));
            datacontext.GetTable<ArticleUser>().InsertOnSubmit(new ArticleUser(user, art));
            datacontext.SubmitChanges();
        }

        /// <summary>
        /// Enregistre un article favori en base
        /// </summary>
        /// <param name="art">article favori</param>
        public void AddFavoris(Article art)
        {
            int fId = datacontext.GetTable<FluxData>().Where(f => f.Link.Trim().Equals(FluxManager.FAVORIS)).First().FluxID;
            datacontext.GetTable<ArticleData>().InsertOnSubmit(new ArticleData(art) { FluxID = fId});
            datacontext.SubmitChanges();
        }

        /// <summary>
        /// Enregistre un article à lire plus tard
        /// </summary>
        /// <param name="art">l'article à enregistrer</param>
        public void AddAlirePlustard(Article art)
        {
            datacontext.GetTable<ArticleData>().InsertOnSubmit(new ArticleData(art));
            datacontext.SubmitChanges();
        }
    }
}

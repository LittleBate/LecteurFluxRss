using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using Buisness;

namespace Data
{
    /// <summary>
    /// Proxy de la classe Flux.
    /// Représentation de la table FluxData dans la base de donnée
    /// </summary>
    [Table]
    public class FluxData : IFlux
    {
        /// <summary>
        /// Constructeur par defaut
        /// </summary>
        public FluxData()
        {
            this.flux = new Flux();
        }

        /// <summary>
        /// ID unique et autogénéré du flux
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int FluxID;
        
        private EntitySet<ArticleData> articles;
        /// <summary>
        /// Association entre le flux et l'ensemble des articles qu'il contient.
        /// </summary>
        [Association(Storage = "articles", OtherKey = "FluxID")]
        public EntitySet<ArticleData> Articles
        {
            get
            {
                return this.articles;
            }
            set
            {
                this.articles.Assign(value);
            }
        }

        /// <summary>
        /// Lien vers le flux rss
        /// </summary>
        [Column(CanBeNull = false)]
        public string Link
        {
            get
            {
                return flux.Link;
            }
            set
            {
                flux.Link = value;
            }
        }

        /// <summary>
        /// Le flux associé à ce proxy
        /// </summary>
        private Flux flux { get;  set;}

        /// <summary>
        /// Getter du flux associé à ce proxy.
        /// Utilisé car nous ne pouvons pas utiliser la propriété
        /// avec les linq pour éditer une list<Flux> à partir d'une List<FluxData>
        /// -> dans le classe SauvegardeManagerBdd.
        /// </summary>
        public Flux getFlux()
        {
            return flux;
        }
    }
}

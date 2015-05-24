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
    /// Table qui représente le partage des article entre utilisateur.
    /// Les articles envoyés et lesarticles reçus.
    /// Classe non fini et non testé
    /// </summary>
    [Table]
    public class ArticleUser
    {
        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="user">Utilisateur</param>
        /// <param name="art"></param>
        public ArticleUser(IUser user, IArticle art)
        {
            User = user;
            Article = art;
        }

        /// <summary>
        /// ID de l'article partagé
        /// </summary>
        [Column(CanBeNull = false)]
        public int ArticleID;

        private EntityRef<IArticle> article;
        /// <summary>
        /// Association avec l'article partagé.
        /// </summary>
        [Association(Storage = "article", ThisKey = "ArticleID")]
        public IArticle Article
        {
            get
            {
                return this.article.Entity;
            }
            set
            {
                this.article.Entity = value;
            }
        }

        /// <summary>
        /// login de l'utilisateur source ou destinataire
        /// </summary>
        [Column(CanBeNull = false)]
        public int UserLogin;

        private EntityRef<IUser> user;
        /// <summary>
        /// Association avec l'utilisateur source ou destinataire
        /// </summary>
        [Association(Storage = "user", ThisKey = "UserLogin")]
        public IUser User
        {
            get
            {
                return this.user.Entity;
            }
            set
            {
                this.user.Entity = value;
            }
        }

        /// <summary>
        /// Définit ou Obtient une valeur qui détermine si l'utilisateur est source ou destinataire
        /// </summary>
        [Column]
        public bool IsEnvoye { get; set; }
    }
}

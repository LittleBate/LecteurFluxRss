using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using Buisness;

namespace Data
{
    /// <summary>
    /// Proxy de la classe Article. 
    /// Représentation de la table ArticleData dans le base de donnéé
    /// </summary>
    [Table]
    public class ArticleData : IArticle
    {
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public ArticleData()
        {
            article = new Article();
        }

        /// <summary>
        /// Constructeur. Prend un article pour en être le proxy
        /// </summary>
        /// <param name="art">article lié</param>
        public ArticleData(Article art)
        {
            article = art;
        }

        /// <summary>
        /// ID unique et autogénéré de l'article
        /// </summary>
        [Column(IsPrimaryKey=true,IsDbGenerated=true)]
        public int ArticleID;
        
        /// <summary>
        /// ID en db du flux contenant l'article
        /// </summary>
        [Column]
        public int FluxID;

        /// <summary>
        /// Titre de l'article
        /// </summary>
        [Column]
        public string Title
        {
            get
            {
                return article.Title;
            }
            set
            {
                article.Title = value;
            }
        }

        /// <summary>
        /// Liens vers l'article
        /// </summary>
        [Column(CanBeNull = true)]
        public string Link
        {
            get
            {
                return article.Link;
            }
            set
            {
                article.Link = value;
            }
        }

        /// <summary>
        /// Contenu de l'article
        /// </summary>
        [Column]
        public string Description
        {
            get
            {
                return article.Description;
            }
            set
            {
                article.Description = value;
            }
        }

        /// <summary>
        /// Auteur de l'article
        /// </summary>
        [Column(CanBeNull = true)]
        public string Author
        {
            get
            {
                return article.Author;
            }
            set
            {
                article.Author = value;
            }
        }

        /// <summary>
        /// Date de publication de l'article
        /// </summary>
        [Column(CanBeNull=true)]
        public DateTime? DatePublication
        {
            get
            {
                return article.DatePublication;
            }
            set
            {
                article.DatePublication = value;
            }
        }

        /// <summary>
        /// Article lié dans le métier
        /// </summary>
        public Article article { get; private set; }
    }
}

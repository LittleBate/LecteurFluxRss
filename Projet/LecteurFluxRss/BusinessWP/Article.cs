using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Buisness
{
    /// <summary>
    /// Représente un article publié dans un flux rss
    /// </summary>
    public class Article : IArticle
    {
        public Article()
        {
            listTag = new List<Tag>();
        }

        private string title;
        /// <summary>
        /// Definit ou Obtient le titre de l'article
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        private string link;
        /// <summary>
        /// Definit ou Obtient le liens vers l'article
        /// </summary>
        public string Link
        {
            get
            {
                return link;
            }
            set
            {
                link = value;
            }
        }

        private string description;
        /// <summary>
        /// Definit ou Obtient la description de l'artile
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                ImageUrl = Regex.Match(value, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                description = Regex.Replace(value, "<.*?>", string.Empty);
            }
        }

        private string author;
        /// <summary>
        /// Definit ou Obtient l'autheur de l'article
        /// </summary>
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }

        private DateTime? datePublication;
        /// <summary>
        /// Definit ou Obtient la date de publication de l'article
        /// </summary>
        public DateTime? DatePublication
        {
            get
            {
                return datePublication;
            }
            set
            {
                datePublication = value;
            }
        }

        private string iconUrl;
        /// <summary>
        /// Definit ou obtient l'url du media associé à l'article
        /// </summary>
        public string IconUrl
        {
            get
            {
                return iconUrl;
            }
            set
            {
                iconUrl = value;
            }
        }

        private string imageUrl;
        /// <summary>
        /// Definit ou obtient l'url de l'image associé à l'article
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set
            {
                imageUrl = value;
            }
        }

        private List<Tag> listTag;
        /// <summary>
        /// Obtient la liste des tags associés à l'article
        /// </summary>
        public IEnumerable<Tag> ListTag
        {
            get
            {
                return new List<Tag>(listTag);
            }
        }

        /// <summary>
        /// Ajoute un tag à l'article
        /// </summary>
        /// <param name="tag">nouveau tag à ajouter</param>
        public void AddTag(Tag tag)
        {
            listTag.Add(tag);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t").Append("Title : ").Append(Title).Append("\n");
            sb.Append("\t").Append("Author : ").Append(Author).Append("\n");
            sb.Append("\t").Append("DatePublication : ").Append(DatePublication).Append("\n");
            sb.Append("\t").Append("Link : ").Append(Link).Append("\n");
            sb.Append("\t").Append("Description : ").Append(Description).Append("\n\n");
            return sb.ToString();
        }


    }
}

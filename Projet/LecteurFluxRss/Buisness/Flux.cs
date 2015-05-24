using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    /// <summary>
    /// Représente un flux rss
    /// </summary>
    public class Flux : IFlux
    {
        public Flux()
        {
            listArticle = new List<Article>();
            listTag = new List<Tag>();
        }

        private string title;
        /// <summary>
        /// Definit ou Obtient le titre du Flux Rss
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
        /// Definit ou Obtient le liens vers l'editeur du flux RSS
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
        /// Definit ou Obtient la descrip^tion du flux Rss
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        private string imagePath;
        /// <summary>
        /// Definit ou Obtient le chemin vers l'image correspondant au flux rss
        /// </summary>
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
            }
        }

        private List<Tag> listTag;
        /// <summary>
        /// Obtient la liste des tags associés au flux
        /// </summary>
        public IEnumerable<Tag> ListTag
        {
            get
            {
                return new List<Tag>(listTag);
            }
        }

        /// <summary>
        /// Ajoute un tag au flux
        /// </summary>
        /// <param name="tag">nouveau tag à ajouter</param>
        public void AddTag(Tag tag)
        {
            listTag.Add(tag);
        }

        private List<Article> listArticle;
        /// <summary>
        /// Obtient la liste des articles publiés dans ce flus rss
        /// </summary>
        public List<Article> ListArticle
        {
            get
            {
                return new List<Article>(listArticle);
            }
        }

        /// <summary>
        /// Ajoute l'article donné en paramètre dans la liste d'article publié e par le flus.
        /// </summary>
        /// <param name="art">Article à ajouter</param>
        public void AddArticle(Article art)
        {
            listArticle.Add(art);
        }

        /// <summary>
        /// Ajoute tous les articles donnés en paramètre dans la liste des articles publiés par le flus
        /// </summary>
        /// <param name="listArticle">ensemble des article à ajouter</param>
        public void AddAllArtciel(IEnumerable<Article> listArticle)
        {
            listArticle.ToList().ForEach(a => AddArticle(a));
        }

        /// <summary>
        /// Supprime l'article donné en paramètre de la liste des articles publié dans le flus
        /// </summary>
        /// <param name="art">Article à supprimer du flux</param>
        public void RemoveArticle(Article art)
        {
            listArticle.Remove(art);
        }

        /// <summary>
        /// Obtient l'index dans la liste des article de l'article donné en paramètre
        /// </summary>
        /// <param name="art">article dont on cherche l'index</param>
        /// <returns>index de l'article</returns>
        public int GetIndexOf(Article art)
        {
            return listArticle.IndexOf(art);
        }

        /// <summary>
        /// Obtient l'article de la liste des article situé à l'index donné en paramètre
        /// </summary>
        /// <param name="index">index cheché</param>
        /// <returns>Article cherché"</returns>
        public Article GetArticleAt(int index)
        {
            return listArticle[index];
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Title : ").Append(Title).Append("\n");
            sb.Append("Link : ").Append(Link).Append("\n");
            sb.Append("Description : ").Append(Description).Append("\n\n");
            sb.Append("Liste Article : \n");
            listArticle.ForEach(a => sb.Append(a));
            return sb.ToString();
        }
    }
}

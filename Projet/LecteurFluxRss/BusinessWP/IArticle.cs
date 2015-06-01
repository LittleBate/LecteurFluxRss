using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public interface IArticle
    {
        /// <summary>
        /// Définit ou obtient le titre de l'article
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Définit ou otient le liens vers l'article
        /// </summary>
        string Link { get; set; }

        /// <summary>
        /// Définit ou obtient a description de l'article
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Définit ou obtient l'auteur de l'article
        /// </summary>
        string Author { get; set; }

        /// <summary>
        /// Définit ou obtient la date de publication de l'article
        /// </summary>
        DateTime? DatePublication { get; set; }
    }
}

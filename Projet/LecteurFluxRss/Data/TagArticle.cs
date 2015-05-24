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
    /// Représente une association entre un article et un tag
    /// </summary>
    public class TagArticle
    {


        /// <summary>
        /// Article portant le tag
        /// </summary>
        [Column(CanBeNull = false)]
        public int ArticleID;

        private EntityRef<IArticle> article;
        /// <summary>
        /// Association avec un article
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
        /// Tag qui décore l'article
        /// </summary>
        [Column(CanBeNull = false)]
        public int TagID;

        private EntityRef<ITag> tag;
        /// <summary>
        /// Association avec un tag
        /// </summary>
        [Association(Storage = "tag", ThisKey = "TagID")]
        public ITag Tag
        {
            get
            {
                return this.tag.Entity;
            }
            set
            {
                this.tag.Entity = value;
            }
        }
    }
}

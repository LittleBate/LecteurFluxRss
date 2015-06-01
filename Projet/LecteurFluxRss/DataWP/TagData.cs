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
    /// Proxy de la classe Tag.
    /// Représentation de la table TagData en Base de donnée
    /// </summary>
    [Table]
    public class TagData : ITag
    {
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public TagData()
        {
            tag = new Tag(String.Empty);
        }

        /// <summary>
        /// ID unique et autogénéré d'un  tag
        /// </summary>
        [Column(IsPrimaryKey=true,IsDbGenerated=true)]
        public int TagID;
        
        /// <summary>
        /// Nom du tag
        /// </summary>
        [Column]
        public string Name
        {
            get
            {
                return tag.Name;
            }
            set
            {
                tag.Name = value;
            }
        }

        /// <summary>
        /// Le tag lié au proxy
        /// </summary>
        private ITag tag;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public class Tag : ITag
    {
        /// <summary>
        /// Nom par defaut d'un tag.
        /// </summary>
        public const string DEFAULT_NAME = "default";

        private string name;
        /// <summary>
        /// Nom du tag. 
        /// Ne dois pas être null ou vide, sinon il prend la valeur par défaut.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    name = DEFAULT_NAME;
                }
                name = value;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">le nom du tag</param>
        public Tag(string name)
        {
            Name = name;
        }

        public override int GetHashCode()
        {
            return name.ToCharArray().First().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Tag)
            {
                Tag t = obj as Tag;
                if (name.Equals(t.name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

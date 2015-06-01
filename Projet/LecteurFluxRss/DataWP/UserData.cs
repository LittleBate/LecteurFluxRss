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
    /// Proxy de la classe User.
    /// Représente la table de User en Base de donnée.
    /// Non terminé et non testé.
    /// </summary>
    [Table]
    public class UserData : IUser
    {
        /// <summary>
        /// Login de l'utilisateur
        /// </summary>
        [Column(IsPrimaryKey=true)]
        public string login;

        /// <summary>
        /// Mot de passe de l'utilisateur (sous forme de hashCode)
        /// </summary>
        [Column]
        public string mdp;


        public string Login
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string MotDePasse
        {
            get
            {
                throw new NotImplementedException();
            }
            private set
            {
            }
        }
    }
}

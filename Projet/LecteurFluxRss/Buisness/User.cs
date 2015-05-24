using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    /// <summary>
    /// Représente un utilisateur.
    /// Classe non terminé. nous avons seulement fait l'implémentation de IUser
    /// </summary>
    public class User : IUser
    {

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
            get { throw new NotImplementedException(); }
        }
    }
}

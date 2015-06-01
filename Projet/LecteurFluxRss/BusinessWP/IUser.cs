using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public interface IUser
    {
        /// <summary>
        /// Définit ou obtient le login de l'utilisateur
        /// </summary>
        string Login { get; set; }
        /// <summary>
        /// Obtient le mot de passe de l'utilisateur (hashCode)
        /// </summary>
        string MotDePasse { get;}
    }
}

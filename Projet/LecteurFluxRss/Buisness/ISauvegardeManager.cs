using Buisness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public interface ISauvegardeManager
    {

        /// <summary>
        /// Chargement de tous les flux enregistré et de leurs articles.
        /// </summary>
        /// <returns>Liste des flux chargés</returns>
        IEnumerable<Flux> Load();

        /// <summary>
        /// Transmet un article à un utilisateur.
        /// </summary>
        /// <param name="art">article à partager</param>
        /// <param name="user">destinataire</param>
        void Partager(Article art, IUser user);

        /// <summary>
        /// Ajoute un article aux favoris 
        /// </summary>
        /// <param name="art">article à ajouter</param>
        void AddFavoris(Article art);

        /// <summary>
        /// Ajoute un article à lire plus tard.
        /// </summary>
        /// <param name="art">article à ajouter</param>
        void AddAlirePlustard(Article art);
    }
}

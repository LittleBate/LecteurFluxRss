using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness
{
    public interface IFlux
    {
        /// <summary>
        /// Définit ou obtient le liens vers le flux rss
        /// </summary>
        string Link { get; set; }
    }
}

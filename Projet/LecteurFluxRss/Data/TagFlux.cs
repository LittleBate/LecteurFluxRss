using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using Buisness;

namespace Data
{
    /// <summary>
    /// Table d'association entre un flux et un tag
    /// </summary>
    public class TagFlux
    {
        /// <summary>
        /// ID du flux associé
        /// </summary>
        [Column(CanBeNull = false)]
        public int FluxID;

        private EntityRef<IFlux> flux;
        /// <summary>
        /// Référence versle flux associé
        /// </summary>
        [Association(Storage = "flux", ThisKey = "FluxID")]
        public IFlux Flux
        {
            get
            {
                return this.flux.Entity;
            }
            set
            {
                this.flux.Entity = value;
            }
        }

        /// <summary>
        /// ID du tag associé
        /// </summary>
        [Column(CanBeNull = false)]
        public int TagID;

        private EntityRef<ITag> tag;
        /// <summary>
        /// Référence vers le tag associé
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

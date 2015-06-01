using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPRssReader.ViewModel
{
    public class VMApplication
    {
        /// <summary>
        /// Instance unique du ViewModel de l'application
        /// </summary>
        private static VMApplication instance;

        /// <summary>
        /// Obtient l'instance unique du ViewModel de l'application
        /// </summary>
        public static VMApplication Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new VMApplication();
                }
                return instance;
            }
        }
        
        private VMApplication()
        {

        }

    }
}

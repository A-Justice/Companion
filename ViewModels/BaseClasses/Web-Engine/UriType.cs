using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    public enum UriType
    {
        /// <summary>
		/// A HTML page with content to parse and to follow the links.
		/// </summary>
		Content,

        /// <summary>
        /// A resource link like images, videos, etc.
        /// </summary>
        Resource
    }
}

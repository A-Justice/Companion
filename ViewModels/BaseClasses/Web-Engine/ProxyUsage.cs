using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.BaseClasses.Web_Engine
{
    public enum ProxyUsage
    {
        /// <summary>
        /// Explicitely use the provided proxy.
        /// </summary>
        UseProxy,

		/// <summary>
		/// Explicitely use no proxy.
		/// </summary>
		NoProxy,

		/// <summary>
		/// Use the system-default proxy.
		/// </summary>
		Default
    }
}

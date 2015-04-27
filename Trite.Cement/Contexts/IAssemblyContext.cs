using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;

namespace Trite.Cement.Contexts
{
    public interface IAssemblyContext
    {
        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        IEnumerable<Assembly> Assemblies { get; } 
    }
}

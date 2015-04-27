using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trite.Cement.Link;
using Trite.Cement.Formats;

namespace Trite.Cement
{
    public interface ITypeReader : ILinkable<Type>
    {
        /// <summary>
        /// Gets a value indicating whether to bypass the current content format.
        /// </summary>
        bool BypassFormat { get; }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object Read(IFormatReader reader);
    }
}

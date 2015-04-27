using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trite.Cement.Link;
using Trite.Cement.Formats;

namespace Trite.Cement
{
    public interface ITypeWriter : ILinkable<Type>
    {
        /// <summary>
        /// Gets a value indicating whether to bypass the current content format.
        /// </summary>
        bool BypassFormat { get; }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        void Write(IFormatWriter writer, object value);
    }
}

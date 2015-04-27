using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trite.Cement.Link;

namespace Trite.Cement.Formats
{
    public interface IFormat
    {
        /// <summary>
        /// Gets a value indicating whether the format supports tags.
        /// </summary>
        bool SupportsTags { get; }
        /// <summary>
        /// Creates a writer.
        /// </summary>
        /// <param name="stream">The stream.</param>
        IFormatWriter CreateWriter(Stream stream);
        /// <summary>
        /// Creates a reader.
        /// </summary>
        /// <param name="stream">The stream.</param>
        IFormatReader CreateReader(Stream stream);
    }
}

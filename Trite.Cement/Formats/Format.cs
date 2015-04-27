using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trite.Cement.Formats.Binary;
using Trite.Cement.Formats.None;
using Trite.Cement.Formats.Xml;

namespace Trite.Cement.Formats
{
    public class Format
    {
        #region Formats
        /// <summary>
        /// Gets an empty format only providing access to the stream itself.
        /// </summary>
        public static IFormat None
        {
            get{ return new FormatlessFormat(); }
        }
        /// <summary>
        /// Gets the binary format.
        /// </summary>
        public static IFormat Binary
        {
            get { return new BinaryFormat(); }
        }
        /// <summary>
        /// Gets the XML format.
        /// </summary>
        public static IFormat Xml
        {
            get{ return new XmlFormat(); }
        }
        #endregion
    }
}

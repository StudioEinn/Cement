using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement
{
    public abstract class TypeReader<T> : ITypeReader
    {
        #region Methods
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public abstract T Read(IFormatReader reader);
        #endregion

        #region ITypeReader Member
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Id
        {
            get { return typeof(T); }
        }
        /// <summary>
        /// Gets a value indicating whether to bypass the current content format.
        /// </summary>
        public virtual bool BypassFormat
        {
            get { return false; }
        }
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object ITypeReader.Read(IFormatReader reader)
        {
            return this.Read(reader);
        }
        #endregion
    }
}

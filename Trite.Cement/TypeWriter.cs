using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement
{
    public abstract class TypeWriter<T> : ITypeWriter
    {
        #region Methods
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The format writer.</param>
        /// <param name="value">The value.</param>
        public abstract void Write(IFormatWriter writer, T value);
        #endregion

        #region ITypeWriter Member
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
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The format writer</param>
        /// <param name="value">The value.</param>
        void ITypeWriter.Write(IFormatWriter writer, object value)
        {
            this.Write(writer, (T)value);
        }
        #endregion
    }
}

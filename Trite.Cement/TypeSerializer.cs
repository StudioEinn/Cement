using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement
{
    public abstract class TypeSerializer<T> : ITypeReader, ITypeWriter
    {
        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public virtual Type Id
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
        #endregion

        #region Methods
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public abstract T Read(IFormatReader reader);
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public abstract void Write(IFormatWriter writer, T value);
        #endregion

        #region ITypeReader Member
        /// <summary>
        /// Reads an instance.
        /// </summary>
        /// <param name="reader">The reader.</param>
        object ITypeReader.Read(IFormatReader reader)
        {
            return this.Read(reader);
        }
        #endregion

        #region ITypeWriter Member
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        void ITypeWriter.Write(IFormatWriter writer, object value)
        {
            this.Write(writer, (T)value);
        }
        #endregion
    }
}

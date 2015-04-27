using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trite.Cement.Formats;

namespace Trite.Cement.Serialization
{
    public class GuidSerializer : TypeSerializer<Guid>
    {
        #region Overrides of TypeSerializer<Guid>
        /// <summary>
        /// Reads the specified guid.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Guid Read(IFormatReader reader)
        {
            return new Guid(reader.ReadString("Value"));
        }
        /// <summary>
        /// Writes the specified guid.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, Guid value)
        {
            writer.WriteString("Value", value.ToString());
        }
        #endregion
    }
}

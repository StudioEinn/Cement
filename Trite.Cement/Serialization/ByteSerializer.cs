using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement.Serialization
{
    public class ByteSerializer : TypeSerializer<byte>
    {
        #region Overrides of ContentTypeSerializer<byte>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override byte Read(IFormatReader reader)
        {
            return reader.ReadByte("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, byte value)
        {
            writer.WriteByte("Value", value);
        }
        #endregion
    }
}

using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement.Serialization
{
    public class IntegerSerializer : TypeSerializer<int>
    {
        #region Overrides of ContentTypeSerializer<int>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override int Read(IFormatReader reader)
        {
            return reader.ReadInteger("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, int value)
        {
            writer.WriteInteger("Value", value);
        }
        #endregion
    }
}

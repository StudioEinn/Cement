using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement.Serialization
{
    public class CharSerializer : TypeSerializer<char>
    {
        #region Overrides of ContentTypeSerializer<char>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override char Read(IFormatReader reader)
        {
            return reader.ReadCharacter("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, char value)
        {
            writer.WriteCharacter("Value", value);
        }
        #endregion
    }
}

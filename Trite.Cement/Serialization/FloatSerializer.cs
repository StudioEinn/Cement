using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement.Serialization
{
    public class FloatSerializer : TypeSerializer<float>
    {
        #region Overrides of ContentTypeSerializer<float>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public override float Read(IFormatReader reader)
        {
            return reader.ReadFloat("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, float value)
        {
            writer.WriteFloat("Value", value);
        }
        #endregion
    }
}

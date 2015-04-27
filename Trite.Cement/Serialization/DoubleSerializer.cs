using System.IO;
using Trite.Cement.Formats;

namespace Trite.Cement.Serialization
{
    public class DoubleSerializer : TypeSerializer<double>
    {
        #region Overrides of ContentTypeSerializer<double>
        /// <summary>
        /// Reads a value out of the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override double Read(IFormatReader reader)
        {
            return reader.ReadDouble("Value");
        }
        /// <summary>
        /// Writes the specified value.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        public override void Write(IFormatWriter writer, double value)
        {
            writer.WriteDouble("Value", value);
        }
        #endregion
    }
}

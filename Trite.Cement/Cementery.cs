using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trite.Cement;
using Trite.Cement.Contexts;
using Trite.Cement.Formats;

namespace Trite.Cement
{
    public static class Cementery
    {
        /// <summary>
        /// Initializes cementery.
        /// </summary>
        static Cementery()
        {
            Serializer = new Serializer();
            Implementations = new Implementations(
                ContextFactory.CreateSingleAssemblyContext<Serializer>()
            );
        }

        /// <summary>
        /// Gets the serializer instance.
        /// </summary>
        public static Serializer Serializer { get; private set; }

        /// <summary>
        /// Gets implemented classes for specified interfaces or types.
        /// </summary>
        public static Implementations Implementations { get; private set; }

        /// <summary>
        /// Loads an object instance of the specified type from a byte array.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <param name="data">Binary data.</param>
        public static object FromByteArray(Type type, byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                return Cementery.Serializer.Load(type, stream);
            }
        }

        /// <summary>
        /// Loads an object instance of the specified type from a byte array.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="data">Binary data.</param>
        public static T FromByteArray<T>(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                return Cementery.Serializer.Load<T>(stream);
            }
        }

        /// <summary>
        /// Serializes the specified object into a byte array.
        /// </summary>
        /// <param name="value">The object instance.</param>
        public static byte[] ToByteArray(object value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Cementery.Serializer.Save(value, stream);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Loads an object instance of the specified type from a base64 encoded string.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <param name="data">Base64 encoded string.</param>
        public static object FromBase64(Type type, string data)
        {
            return Cementery.FromByteArray(type, Convert.FromBase64String(data));
        }

        /// <summary>
        /// Loads an object instance of the specified type from a base64 encoded string.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="data">Base64 encoded string.</param>
        public static T FromBase64<T>(string data)
        {
            return Cementery.FromByteArray<T>(Convert.FromBase64String(data));
        }

        /// <summary>
        /// Serializes the specified object into a base64 encoded string.
        /// </summary>
        /// <param name="value">The object instance.</param>
        public static string ToBase64(object value)
        {
            return Convert.ToBase64String(Cementery.ToByteArray(value));
        }

        /// <summary>
        /// Loads an object instance of the specified type from an xml string.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <param name="data">Xml string.</param>
        public static object FromXml(Type type, string data)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(data)))
            {
                return Cementery.Serializer.Load(type, stream, Format.Xml);
            }
        }

        /// <summary>
        /// Loads an object instance of the specified type from an xml string.
        /// </summary>
        /// <param name="type">The object type.</param>
        /// <param name="data">Xml string.</param>
        public static T FromXml<T>(string data)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(data)))
            {
                return Cementery.Serializer.Load<T>(stream, Format.Xml);
            }
        }
        
        /// <summary>
        /// Serializes the specified object into an xml string.
        /// </summary>
        /// <param name="value">The object instance.</param>
        public static string ToXml(object value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Cementery.Serializer.Save(value, stream, Format.Xml);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }
    }
}

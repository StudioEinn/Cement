using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Trite.Cement.Formats;
using Trite.Cement.Formats.Binary;
using Trite.Cement.Formats.None;
using Trite.Cement.Layouts;
using Trite.Cement.Layouts.Generation;
using Trite.Cement.Serialization;
using Trite.Cement.Link;
using Trite.Cement.Utility;

namespace Trite.Cement
{
    public class Serializer
    {
        #region Constructors
        internal Serializer()
        {
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Gets a reader or writer for the specified type.
        /// </summary>
        /// <typeparam name="T">The reader or writer type.</typeparam>
        /// <param name="type">The type.</param>
        private T Get<T>(Type type) where T : class, ILinkable<Type>
        {
            foreach (Type baseType in Reflection.GetInheritedTypes(type))
            {
                T instance = Cementery.Implementations.Get<Type, T>(baseType);

                if (instance != null)
                    return instance;
            }

            return new AutomaticLayoutSerializer(type) as T;
        }
        /// <summary>
        /// Gets the content reader for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private ITypeReader GetReader(Type type)
        {
            return this.Get<ITypeReader>(type);
        }
        /// <summary>
        /// Gets the content writer for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private ITypeWriter GetWriter(Type type)
        {
            return this.Get<ITypeWriter>(type);
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="stream">The stream.</param>
        public T Load<T>(Stream stream)
        {
            return this.Load<T>(stream, Format.Binary);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <typeparam name="T">The content type.</typeparam>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        public T Load<T>(Stream stream, IFormat format)
        {
            return (T)this.Load(typeof(T), stream, format);
        }
        /// <summary>
        /// Loads the specified reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        public T Load<T>(IFormatReader reader)
        {
            return (T)this.Load(typeof(T),reader);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        public object Load(Type type, Stream stream)
        {
            return this.Load(type, stream, Format.Binary);
        }
        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        public object Load(Type type, Stream stream, IFormat format)
        {
            ITypeReader reader = this.GetReader(type);
            if (reader.BypassFormat)
            {
                format = Format.None;
            }

            using (IFormatReader formatReader = format.CreateReader(stream))
            {
                return reader.Read(formatReader);
            }
        }
        /// <summary>
        /// Loads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="reader">The reader.</param>
        public object Load(Type type, IFormatReader reader)
        {
            return this.GetReader(type).Read(reader);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        public void Save(object value, Stream stream)
        {
            this.Save(value, stream, Format.Binary);
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="stream">The stream.</param>
        /// <param name="format">The format.</param>
        public void Save(object value, Stream stream, IFormat format)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            ITypeWriter contentWriter = this.GetWriter(value.GetType());
            if (contentWriter.BypassFormat)
            {
                format = Format.None;
            }

            using (IFormatWriter formatWriter = format.CreateWriter(stream))
            {
                contentWriter.Write(formatWriter, value);
            }
        }
        /// <summary>
        /// Saves the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        public void Save(object value, IFormatWriter writer)
        {
            this.GetWriter(value.GetType()).Write(writer, value);
        }
        #endregion
    }
}

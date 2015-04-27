using System;
using System.Reflection;
using Trite.Cement.Formats;
using Trite.Cement.Utility;

namespace Trite.Cement.Layouts.References
{
    internal class ReferenceElement : BaseElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceElement"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="property">The property.</param>
        public ReferenceElement(string tag, PropertyInfo property) 
            : this(tag, property.PropertyType, PropertyHelper.Get(property), PropertyHelper.Set(property))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceElement" /> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="type">The type.</param>
        /// <param name="getAction">The get action.</param>
        /// <param name="setAction">The set action.</param>
        public ReferenceElement(string tag, Type type, Func<object, object> getAction, Action<object, object> setAction)
            : base(tag, getAction, setAction)
        {
            this.Type = type;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type.
        /// </summary>
        public Type Type { get; private set; }
        #endregion

        #region Overrides of PropertyElement
        /// <summary>
        /// Writes property for the specified container.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="container">The container.</param>
        public override void Write(IFormatWriter writer, object container)
        {
            using (writer.Section(this.Tag))
            {
                Cementery.Serializer.Save(this.GetAction(container), writer);
            }
        }
        /// <summary>
        /// Reads the property for the specified container.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="container">The container.</param>
        public override void Read(IFormatReader reader, object container)
        {
            using (reader.Section(this.Tag))
            {
                this.SetAction(container, Cementery.Serializer.Load(this.Type, reader));
            }
        }
        #endregion
    }
}

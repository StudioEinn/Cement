using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trite.Cement.Layouts
{
    public interface IElementContainer
    {
        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        void Add(ILayoutElement element);
    }
}

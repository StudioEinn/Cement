using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trite.Cement.Layouts.Collections
{
    [Flags]
    public enum Scope
    {
        None = 0,
        Collection = 1,
        Element = 2,
        All = Collection | Element
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trite.Cement
{
    public static class Extensions
    {
        public static bool HasFlag(this Enum keys, Enum flag)
        {
            ulong keysVal = Convert.ToUInt64(keys);
            ulong flagVal = Convert.ToUInt64(flag);

            return (keysVal & flagVal) == flagVal;
        }
    }
}

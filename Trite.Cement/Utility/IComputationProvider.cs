using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trite.Cement.Utility
{
    public interface IComputationProvider<in TKey, out TValue>
    {
        /// <summary>
        /// Computes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        TValue Compute(TKey key);
    }
}

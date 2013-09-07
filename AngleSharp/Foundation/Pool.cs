using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp
{
    /// <summary>
    /// Provides a pool of used / recycled resources.
    /// </summary>
    static class Pool
    {
        #region Members

        static Stack<StringBuilder> _builder;
        static Object _lock;

        #endregion

        #region ctor

        static Pool()
        {
            _builder = new Stack<StringBuilder>();
            _lock = new Object();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Either creates a fresh stringbuilder or gets a (cleaned) used one.
        /// </summary>
        /// <returns>A stringbuilder to use.</returns>
        public static StringBuilder NewStringBuilder()
        {
            lock (_lock)
            {
                if (_builder.Count == 0)
                    return new StringBuilder();

                return _builder.Pop().Clear();
            }
        }

        /// <summary>
        /// Returns the given stringbuilder to the pool and gets the current
        /// string content.
        /// </summary>
        /// <param name="sb">The stringbuilder to recycle.</param>
        /// <returns>The string that is contained in the stringbuilder.</returns>
        public static String Return(this StringBuilder sb)
        {
            lock (_lock)
            {
                _builder.Push(sb);
            }

            return sb.ToString();
        }

        #endregion
    }
}

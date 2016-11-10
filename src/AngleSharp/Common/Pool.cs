namespace AngleSharp.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Provides a pool of used / recycled resources.
    /// </summary>
    public static class Pool
    {
        private static readonly Stack<StringBuilder> _builder = new Stack<StringBuilder>();
        private static readonly Object _lock = new Object();

        /// <summary>
        /// Either creates a fresh stringbuilder or gets a (cleaned) used one.
        /// </summary>
        /// <returns>A stringbuilder to use.</returns>
        public static StringBuilder NewStringBuilder()
        {
            lock (_lock)
            {
                if (_builder.Count == 0)
                {
                    return new StringBuilder(1024);
                }

                return _builder.Pop().Clear();
            }
        }

        /// <summary>
        /// Returns the given stringbuilder to the pool and gets the current
        /// string content.
        /// </summary>
        /// <param name="sb">The stringbuilder to recycle.</param>
        /// <returns>The string that is created in the stringbuilder.</returns>
        public static String ToPool(this StringBuilder sb)
        {
            var result = sb.ToString();

            lock (_lock)
            {
                _builder.Push(sb);
            }

            return result;
        }
    }
}

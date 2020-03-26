namespace AngleSharp.Text
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Provides a pool of used / recycled resources.
    /// </summary>
    public static class StringBuilderPool
    {
        private static readonly Stack<StringBuilder> _builder = new Stack<StringBuilder>();
        private static readonly Object _lock = new Object();
        private static Int32 _count = 4;
        private static Int32 _limit = 85000;

        /// <summary>
        /// Gets or sets the maximum number of instances - at least 1.
        /// </summary>
        public static Int32 MaxCount
        {
            get => _count;
            set => _count = Math.Max(1, value);
        }

        /// <summary>
        /// Gets or sets the max. capacity per instance - at least 85000.
        /// </summary>
        public static Int32 SizeLimit
        {
            get => _limit;
            set => _limit = Math.Max(1024, value);
        }

        /// <summary>
        /// Either creates a fresh stringbuilder or gets a (cleaned) used one.
        /// </summary>
        /// <returns>A stringbuilder to use.</returns>
        public static StringBuilder Obtain()
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
                var current = _builder.Count;

                if (sb.Capacity > _limit)
                {
                    // Drop large instances
                }
                else if (current == _count)
                {
                    DropMinimum(sb);
                }
                else if (current < Math.Min(2, _count) || _builder.Peek().Capacity < sb.Capacity)
                {
                    _builder.Push(sb);
                }
            }

            return result;
        }

        private static void DropMinimum(StringBuilder sb)
        {
            var minimum = sb.Capacity;
            var instances = _builder.ToArray();
            var index = FindIndex(instances, minimum);

            if (index > -1)
            {
                RebuildPool(sb, instances, index);
            }
        }

        private static void RebuildPool(StringBuilder sb, StringBuilder[] instances, Int32 index)
        {
            _builder.Clear();
            var i = instances.Length - 1;

            while (i > index)
            {
                _builder.Push(instances[i--]);
            }

            while (i > 0)
            {
                _builder.Push(instances[--i]);
            }

            _builder.Push(sb);
        }

        private static Int32 FindIndex(StringBuilder[] instances, Int32 minimum)
        {
            var index = -1;

            for (var i = 0; i < instances.Length; i++)
            {
                var capacity = instances[i].Capacity;

                if (capacity < minimum)
                {
                    minimum = capacity;
                    index = i;
                }
            }

            return index;
        }
    }
}

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
        private static readonly Stack<StringBuilder> _builder = new ();
        private static readonly Object _lock = new();
        private static Int32 _count = 4;
        private static Int32 _limit = 85000;
        private static Boolean _isPoolingDisabled = false;
        private const Int32 _defaultStringBuilderSize = 1024;

        /// <summary>
        /// Gets or sets whether string builder pooling is disabled.  When disabled, Obtain() will always return a new instance.
        /// Disabling will increase memory usage and GC pressure, but may improve performance in scenarios with high
        /// parallel processing.
        /// </summary>
        public static Boolean IsPoolingDisabled
        {
            get => _isPoolingDisabled;
            set => _isPoolingDisabled = value;
        }

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
        /// Either creates a fresh stringbuilder or gets a (cleaned) used one.  If <see cref="IsPoolingDisabled"/> is set to true, a new instance is always returned."/>
        /// </summary>
        /// <returns>A stringbuilder to use.</returns>
        public static StringBuilder Obtain()
        {
            StringBuilder result = null;

            if (_isPoolingDisabled)
            {
                result = CreateStringBuilder();
            }
            else
            {
                lock (_lock)
                {
                    if (_builder.Count == 0)
                    {
                        result = CreateStringBuilder();
                    }
                    else
                    {
                        result = _builder.Pop().Clear();
                    }
                }
            }

            return result;
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
            ReturnToPool(sb);
            return result;
        }

        /// <summary>
        /// Creates a new StringBuilder with a default capacity of 1024.
        /// </summary>
        /// <returns>A StringBuilder instance.</returns>
        internal static StringBuilder CreateStringBuilder() => new StringBuilder(1024);

        /// <summary>
        /// Returns the given stringbuilder to the pool.
        /// </summary>
        /// <param name="sb">The stringbuilder to recycle.</param>
        internal static void ReturnToPool(this StringBuilder sb)
        {
            if (!_isPoolingDisabled)
            {
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
            }
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

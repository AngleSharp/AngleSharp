namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of values in the CSS context.
    /// </summary>
    sealed class CssValueList : ICssValue, IEnumerable<ICssValue>
    {
        #region Fields

        readonly List<ICssValue> _items;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        internal CssValueList()
        {
            _items = new List<ICssValue>();
        }

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="item">The first item to add.</param>
        internal CssValueList(ICssValue item)
			: this()
        {
			_items.Add(item);
        }

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="items">The items to use.</param>
        internal CssValueList(List<ICssValue> items)
        {
            _items = items;
        }

		#endregion

        #region Properties

        /// <summary>
        /// Gets the number of CSSValues in the list.
        /// </summary>
        public Int32 Length
        {
            get { return _items.Count; }
        }

        /// <summary>
        /// Gets the type of the value (list).
        /// </summary>
        public CssValueType Type
        {
            get { return CssValueType.List; }
        }

        /// <summary>
        /// Gets a CSS code representation of the stylesheet.
        /// </summary>
        public String CssText
        {
            get
            {
                var builder = Pool.NewStringBuilder();

                if (_items.Count > 0)
                {
                    builder.Append(_items[0].CssText);

                    for (int i = 1; i < _items.Count; i++)
                    {
                        if (_items[i] == CssValue.Separator)
                            builder.Append(',');
                        else
                            builder.Append(' ').Append(_items[i].CssText);
                    }
                }

                return builder.ToPool();
            }
        }


        /// <summary>
        /// Used to retrieve a CSSValue by ordinal index. The order in this collection represents the order of the values in the CSS style property.
        /// </summary>
        /// <param name="index">If index is greater than or equal to the number of values in the list, this returns null.</param>
        /// <returns>The value at the given index or null.</returns>
        public ICssValue this[Int32 index]
        {
            get { return index >= 0 && index < _items.Count ? _items[index] : null; }
        }

        #endregion

        #region Methods

        public void Add(ICssValue item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void RemoveAt(Int32 index)
        {
            _items.RemoveAt(index);
        }

        public void RemoveRange(Int32 index, Int32 count)
        {
            _items.RemoveRange(index, count);
        }

        #endregion

        #region IEnumerable

        public IEnumerator<ICssValue> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of values in the CSS context.
    /// </summary>
    sealed class CSSValueList : CSSValue, IEnumerable<ICssValue>
    {
        #region Fields

        readonly List<ICssValue> _items;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        internal CSSValueList()
            : base(CssValueType.List)
        {
            _items = new List<ICssValue>();
        }

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="item">The first item to add.</param>
        internal CSSValueList(ICssValue item)
			: this()
        {
			_items.Add(item);
        }

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="items">The items to use.</param>
        internal CSSValueList(List<ICssValue> items)
            : base(CssValueType.List)
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

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            var builder = Pool.NewStringBuilder();

            if (_items.Count > 0)
            {
                builder.Append(_items[0].CssText);

                for (int i = 1; i < _items.Count; i++)
                {
                    if (_items[i] == CSSValue.Separator)
                        builder.Append(',');
                    else
                        builder.Append(' ').Append(_items[i].CssText);
                }
            }

            return builder.ToPool();
        }

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

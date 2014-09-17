namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of values in the CSS context.
    /// </summary>
    sealed class CSSValueList : CSSValue, ICollection<CSSValue>, ICssValueList
    {
        #region Fields

        readonly List<CSSValue> _items;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        internal CSSValueList()
            : base(CssValueType.List)
        {
            _items = new List<CSSValue>();
        }

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="item">The first item to add.</param>
        internal CSSValueList(CSSValue item)
			: this()
        {
			_items.Add(item);
        }

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="items">The items to use.</param>
        internal CSSValueList(List<CSSValue> items)
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
        public CSSValue this[Int32 index]
        {
            get { return index >= 0 && index < _items.Count ? _items[index] : null; }
        }

        ICssValue ICssValueList.this[Int32 index]
        {
            get { return this[index]; }
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
                builder.Append(_items[0].ToCss());

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

        #endregion

        #region ICollection

        public void Add(CSSValue item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public Boolean Contains(CSSValue item)
        {
            return _items.Contains(item);
        }

        void ICollection<CSSValue>.CopyTo(CSSValue[] array, Int32 arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        Int32 ICollection<CSSValue>.Count
        {
            get { return _items.Count; }
        }

        Boolean ICollection<CSSValue>.IsReadOnly
        {
            get { return false; }
        }

        public Boolean Remove(CSSValue item)
        {
            return _items.Remove(item);
        }

        #endregion

        #region IEnumerable

        public IEnumerator<CSSValue> GetEnumerator()
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

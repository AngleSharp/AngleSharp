using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a list of values in the CSS context.
    /// </summary>
    sealed class CSSValueList : CSSValue
    {
        #region Members

        List<CSSValue> _items;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        internal CSSValueList()
        {
            _items = new List<CSSValue>();
            _type = CssValue.ValueList;
        }

        /// <summary>
        /// Creates a new CSS value list.
        /// </summary>
        /// <param name="items">The list of values to consider.</param>
        internal CSSValueList(List<CSSValue> items)
        {
            _items = items;
            _type = CssValue.ValueList;
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
        [IndexerName("ListItems")]
        public CSSValue this[Int32 index]
        {
            get { return index >= 0 && index < _items.Count ? _items[index] : null; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Used to retrieve a CSSValue by ordinal index. The order in this collection represents the order of the values in the CSS style property.
        /// </summary>
        /// <param name="index">If index is greater than or equal to the number of values in the list, this returns null.</param>
        /// <returns>The value at the given index or null.</returns>
        public CSSValue Item(Int32 index)
        {
            return this[index];
        }

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            var values = new String[_items.Count];

            for (int i = 0; i < _items.Count; i++)
                values[i] = _items[i].CssText;

            return String.Join(" ", values);
        }

        #endregion
    }
}

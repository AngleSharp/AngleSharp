using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using AngleSharp.DOM.Css;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// A collection of CSS elements.
    /// </summary>
    [DOM("StyleSheetList")]
    public sealed class StyleSheetList : IEnumerable<StyleSheet>
    {
        #region Members

        List<StyleSheet> _styleSheets;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new stylesheet class.
        /// </summary>
        internal StyleSheetList()
        {
            _styleSheets = new List<StyleSheet>();
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the stylesheet at the specified index.
        /// If index is greater than or equal to the number
        /// of style sheets in the list, this returns null.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The stylesheet.</returns>
        [DOM("item")]
        public StyleSheet this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= _styleSheets.Count)
                    return null;

                return _styleSheets[index];
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements in the list of stylesheets.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return _styleSheets.Count; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Adds a stylesheet to the list.
        /// </summary>
        /// <param name="styleSheet">The stylesheet to consider.</param>
        internal void Add(StyleSheet styleSheet)
        {
            _styleSheets.Add(styleSheet);
        }

        /// <summary>
        /// Removes a stylesheet from the list.
        /// </summary>
        /// <param name="styleSheet">The stylesheet to remove.</param>
        internal void Remove(StyleSheet styleSheet)
        {
            _styleSheets.Remove(styleSheet);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an enumerator that iterates through the stylesheets.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<StyleSheet> GetEnumerator()
        {
            return _styleSheets.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_styleSheets).GetEnumerator();
        }

        #endregion
    }
}

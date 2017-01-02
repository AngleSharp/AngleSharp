﻿namespace AngleSharp.Dom
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A collection of CSS elements.
    /// </summary>
    sealed class StyleSheetList : IStyleSheetList
    {
        #region Fields

        readonly IEnumerable<IStyleSheet> _sheets;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new stylesheet class.
        /// </summary>
        /// <param name="sheets">The list to enumerate.</param>
        internal StyleSheetList(IEnumerable<IStyleSheet> sheets)
        {
            _sheets = sheets;
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
        public IStyleSheet this[Int32 index]
        {
            get { return _sheets.Skip(index).FirstOrDefault(); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of elements in the list of stylesheets.
        /// </summary>
        public Int32 Length
        {
            get { return _sheets.Count(); }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an enumerator that iterates through the stylesheets.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<IStyleSheet> GetEnumerator()
        {
            return _sheets.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>The enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

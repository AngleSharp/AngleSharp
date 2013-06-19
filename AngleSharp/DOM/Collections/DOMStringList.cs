using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a string list.
    /// </summary>
    [DOM("StringList")]
    public sealed class DOMStringList : List<String>
    {
        #region ctor

        internal DOMStringList()
        {
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The 0-based index of the element.</param>
        /// <returns>The element or null.</returns>
        [DOM("item")]
        public new String this[Int32 index]
        {
            get { return index >= 0 && index < Count ? base[index] : null; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of entries.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return Count; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a boolean indicating if the specified entry is available.
        /// </summary>
        /// <param name="entry">The entry that will be looked for.</param>
        /// <returns>True if the element is available, otherwise false.</returns>
        [DOM("contains")]
        public new Boolean Contains(String entry)
        {
            return base.Contains(entry);
        }

        #endregion
    }
}

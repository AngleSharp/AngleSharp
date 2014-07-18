namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents a string list.
    /// </summary>
    public sealed class DOMStringList : IStringList
    {
        #region Fields

        IEnumerable<String> _list;

        #endregion

        #region ctor

        internal DOMStringList(IEnumerable<String> list)
        {
            _list = list;
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The 0-based index of the element.</param>
        /// <returns>The element or null.</returns>
        public String this[Int32 index]
        {
            get
            {
                var count = 0;

                foreach (var element in _list)
                {
                    if (count == index)
                        return element;
                }

                return null;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of entries.
        /// </summary>
        public Int32 Length
        {
            get 
            {
                var count = 0;

                foreach (var element in _list)
                    count++;

                return count;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a boolean indicating if the specified entry is available.
        /// </summary>
        /// <param name="entry">The entry that will be looked for.</param>
        /// <returns>True if the element is available, otherwise false.</returns>
        public Boolean Contains(String entry)
        {
            foreach (var _entry in _list)
                if (_entry == entry)
                    return true;

            return false;
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Gets the enumerator over all stylesheet titles.
        /// </summary>
        /// <returns>The iterator instance.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Gets the non-generic enumerator.
        /// </summary>
        /// <returns>An iterator over all stylesheet titles.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

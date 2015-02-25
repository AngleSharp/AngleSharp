namespace AngleSharp.Dom.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a string list.
    /// </summary>
    sealed class StringList : IStringList
    {
        #region Fields

        readonly IEnumerable<String> _list;

        #endregion

        #region ctor

        internal StringList(IEnumerable<String> list)
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
            get { return _list.Skip(index).FirstOrDefault(); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of entries.
        /// </summary>
        public Int32 Length
        {
            get { return _list.Count(); }
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
            return _list.Contains(entry);
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
            return _list.GetEnumerator();
        }

        #endregion
    }
}

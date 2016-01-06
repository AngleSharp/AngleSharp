namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
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

        public String this[Int32 index]
        {
            get { return _list.GetItemByIndex(index); }
        }

        #endregion

        #region Properties

        public Int32 Length
        {
            get { return _list.Count(); }
        }

        #endregion

        #region Methods

        public Boolean Contains(String entry)
        {
            return _list.Contains(entry);
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<String> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }
}

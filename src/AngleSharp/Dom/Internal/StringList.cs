namespace AngleSharp.Dom
{
    using AngleSharp.Common;
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

        private readonly IEnumerable<String> _list;

        #endregion

        #region ctor

        internal StringList(IEnumerable<String> list)
        {
            _list = list;
        }

        #endregion

        #region Index

        public String this[Int32 index] => _list.GetItemByIndex(index);

        #endregion

        #region Properties

        public Int32 Length => _list.Count();

        #endregion

        #region Methods

        public Boolean Contains(String entry) => _list.Contains(entry);

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<String> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        #endregion
    }
}

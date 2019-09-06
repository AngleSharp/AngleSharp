namespace AngleSharp.Io.Dom
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a container for file entries captured by the file
    /// upload field.
    /// </summary>
    sealed class FileList : IFileList
    {
        #region Fields

        private readonly List<IFile> _entries;

        #endregion

        #region ctor

        internal FileList()
        {
            _entries = new List<IFile>();
        }

        #endregion

        #region Index

        public IFile this[Int32 index] => _entries[index];

        #endregion

        #region Properties

        public Int32 Length => _entries.Count;

        #endregion

        #region Methods

        public void Add(IFile item) => _entries.Add(item);

        public void Clear() => _entries.Clear();

        public Boolean Remove(IFile item) => _entries.Remove(item);

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<IFile> GetEnumerator() => _entries.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}

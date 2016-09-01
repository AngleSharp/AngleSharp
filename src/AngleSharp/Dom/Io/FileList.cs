namespace AngleSharp.Dom.Io
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

        public IFile this[Int32 index]
        {
            get { return _entries[index]; }
        }

        #endregion

        #region Properties
        
        public Int32 Length
        {
            get { return _entries.Count; }
        }

        #endregion

        #region Methods

        public void Add(IFile item)
        {
            _entries.Add(item);
        }

        public void Clear()
        {
            _entries.Clear();
        }

        public Boolean Remove(IFile item)
        {
            return _entries.Remove(item);
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<IFile> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

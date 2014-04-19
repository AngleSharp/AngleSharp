namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a container for file entries captured by the file
    /// upload field.
    /// </summary>
    [DOM("FileList")]
    public class FileList : IEnumerable<FileEntry>, ICollection<FileEntry>
    {
        #region Fields

        List<FileEntry> _entries;

        #endregion

        #region ctor

        internal FileList()
        {
            _entries = new List<FileEntry>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of contained files.
        /// </summary>
        public Int32 Count
        {
            get { return _entries.Count; }
        }

        /// <summary>
        /// Gets the readonly status of the files.
        /// </summary>
        public Boolean IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the enumerator to iterate over all the stored file entries.
        /// </summary>
        /// <returns>The list's enumerator.</returns>
        public IEnumerator<FileEntry> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(FileEntry item)
        {
            _entries.Add(item);
        }

        public void Clear()
        {
            _entries.Clear();
        }

        public Boolean Contains(FileEntry item)
        {
            return _entries.Contains(item);
        }

        public void CopyTo(FileEntry[] array, Int32 arrayIndex)
        {
            _entries.CopyTo(array, arrayIndex);
        }

        public Boolean Remove(FileEntry item)
        {
            return _entries.Remove(item);
        }

        #endregion
    }
}

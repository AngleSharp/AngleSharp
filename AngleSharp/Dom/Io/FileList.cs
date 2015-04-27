namespace AngleSharp.Dom.Io
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a container for file entries captured by the file
    /// upload field.
    /// </summary>
    sealed class FileList : IEnumerable<IFile>, IFileList
    {
        #region Fields

        readonly List<IFile> _entries;

        #endregion

        #region ctor

        internal FileList()
        {
            _entries = new List<IFile>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the entry at the specified position.
        /// </summary>
        /// <param name="index">The index of the entry.</param>
        /// <returns>The file object.</returns>
        public IFile this[Int32 index]
        {
            get { return _entries[index]; }
        }

        /// <summary>
        /// Gets the number of contained files.
        /// </summary>
        public Int32 Count
        {
            get { return _entries.Count; }
        }

        /// <summary>
        /// Gets the number of contained files.
        /// </summary>
        public Int32 Length
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
        public IEnumerator<IFile> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds another file entry to the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(IFile item)
        {
            _entries.Add(item);
        }

        /// <summary>
        /// Resets the list of file entries.
        /// </summary>
        public void Clear()
        {
            _entries.Clear();
        }

        /// <summary>
        /// Checks if the given item has already been added.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>True if the item is already in the list of files.</returns>
        public Boolean Contains(IFile item)
        {
            return _entries.Contains(item);
        }

        /// <summary>
        /// Copies the list of files to the given array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The start index in the array.</param>
        public void CopyTo(IFile[] array, Int32 arrayIndex)
        {
            _entries.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the given file entry from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item could be removed, otherwise false.</returns>
        public Boolean Remove(IFile item)
        {
            return _entries.Remove(item);
        }

        #endregion
    }
}

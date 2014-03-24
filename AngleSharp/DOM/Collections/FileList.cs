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
    public class FileList : IEnumerable<FileEntry>
    {
        List<FileEntry> _entries;

        internal FileList()
        {
            _entries = new List<FileEntry>();
        }

        /// <summary>
        /// Gets the number of contained files.
        /// </summary>
        public Int32 Length
        {
            get { return _entries.Count; }
        }

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
    }
}

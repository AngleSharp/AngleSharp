using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a container for file entries captured by the file
    /// upload field.
    /// </summary>
    [DOM("FileList")]
    public class FileList : IEnumerable<FileEntry>
    {
        List<FileEntry> _entries;

        public FileList()
        {
            _entries = new List<FileEntry>();
        }

        public Int32 Length
        {
            get { return _entries.Count; }
        }

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

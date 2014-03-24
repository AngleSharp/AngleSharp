namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a file captured in the FileList.
    /// </summary>
    [DOM("FileEntry")]
    public sealed class FileEntry
    {
        internal FileEntry()
        {
        }

        /// <summary>
        /// Gets the file name for the captured file.
        /// </summary>
        public String FileName
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the mime-type for the captured file.
        /// </summary>
        public String Type
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the body / content of the captured file.
        /// </summary>
        public Byte[] Body
        {
            get;
            internal set;
        }
    }
}

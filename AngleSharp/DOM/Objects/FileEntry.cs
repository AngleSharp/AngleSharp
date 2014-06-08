namespace AngleSharp.DOM
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a file captured in the FileList.
    /// </summary>
    [DomName("FileEntry")]
    public sealed class FileEntry
    {
        #region ctor

        internal FileEntry()
        {
        }

        /// <summary>
        /// Creates a new file entry from the given file properties.
        /// </summary>
        /// <param name="name">The name of the file (not the full path).</param>
        /// <param name="body">The stream of bytes of the file's contents.</param>
        /// <returns>The file entry.</returns>
        public static FileEntry FromFile(String name, Stream body)
        {
            using (var ms = new MemoryStream())
            {
                body.CopyTo(ms);
                return new FileEntry { FileName = name, Type = MimeTypes.FromExtension(Path.GetExtension(name)), Body = ms.ToArray() };
            }
        }

        #endregion

        #region Properties

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

        #endregion
    }
}

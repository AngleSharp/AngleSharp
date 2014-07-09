namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Io;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a file captured in the FileList.
    /// </summary>
    sealed class FileEntry : IFile
    {
        #region ctor

        internal FileEntry()
        {
            LastModified = DateTime.Now;
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
                return new FileEntry { Name = name, Type = MimeTypes.FromExtension(Path.GetExtension(name)), Body = ms.ToArray() };
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the file name for the captured file.
        /// </summary>
        public String Name
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

        /// <summary>
        /// Gets the last modified date.
        /// </summary>
        public DateTime LastModified
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the length of the content.
        /// </summary>
        public Int32 Length
        {
            get { return Body != null ? Body.Length : 0; }
        }

        /// <summary>
        /// Gets if the stream is closed.
        /// </summary>
        public Boolean IsClosed
        {
            get { return true; }
        }

        #endregion

        #region Methods

        public IBlob Slice(Int32 start = 0, Int32 end = Int32.MaxValue, String contentType = null)
        {
            var s = Math.Max(start, 0);
            var body = new Byte[Math.Max(Math.Min(Length, end) - s, 0)];

            for (int i = 0; i < body.Length; i++)
                body[i] = Body[s++];

            return new FileEntry { Body = body, Type = contentType ?? Type };
        }

        public void Close()
        {
            //TODO
        }

        #endregion
    }
}

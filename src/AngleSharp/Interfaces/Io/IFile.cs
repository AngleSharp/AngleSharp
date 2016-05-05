namespace AngleSharp.Dom.Io
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a concrete file.
    /// http://dev.w3.org/2006/webapi/FileAPI/#dfn-file
    /// </summary>
    [DomName("File")]
    public interface IFile : IBlob
    {
        /// <summary>
        /// Gets the file's name.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets the last modified date of the file.
        /// </summary>
        [DomName("lastModified")]
        DateTime LastModified { get; }
    }
}

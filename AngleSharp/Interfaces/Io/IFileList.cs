namespace AngleSharp.DOM.Io
{
    using System;

    /// <summary>
    /// Represents a list of files.
    /// http://dev.w3.org/2006/webapi/FileAPI/#dfn-filelist
    /// </summary>
    [DomName("FileList")]
    public interface IFileList
    {
        /// <summary>
        /// Gets the file at the specified index.
        /// </summary>
        /// <param name="index">The index of the file.</param>
        /// <returns>The file at the provided index.</returns>
        [DomName("item")]
        IFile this[Int32 index] { get; }

        /// <summary>
        /// Gets the number of files in the list.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }
    }
}

namespace AngleSharp.Dom.Io
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of files.
    /// http://dev.w3.org/2006/webapi/FileAPI/#dfn-filelist
    /// </summary>
    [DomName("FileList")]
    public interface IFileList : IEnumerable<IFile>
    {
        /// <summary>
        /// Gets the file at the specified index.
        /// </summary>
        /// <param name="index">The index of the file.</param>
        /// <returns>The file at the provided index.</returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        IFile this[Int32 index] { get; }

        /// <summary>
        /// Gets the number of files in the list.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Adds a file to the list.
        /// </summary>
        /// <param name="file">The file to add.</param>
        void Add(IFile file);

        /// <summary>
        /// Removes a file from the list.
        /// </summary>
        /// <param name="file">The file to remove.</param>
        /// <returns>
        /// True if the file was part of the list, otherwise false.
        /// </returns>
        Boolean Remove(IFile file);
    }
}

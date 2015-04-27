namespace AngleSharp.Dom.Io
{
    using AngleSharp.Attributes;
    using System;
    using System.IO;


    /// <summary>
    /// Represents a binary large object.
    /// http://dev.w3.org/2006/webapi/FileAPI/#dfn-Blob
    /// </summary>
    [DomName("Blob")]
    public interface IBlob : IDisposable
    {
        /// <summary>
        /// Gets the length of the blob.
        /// </summary>
        [DomName("size")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the mime-type of the blob.
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets if the stream to the blob is closed.
        /// </summary>
        [DomName("isClosed")]
        Boolean IsClosed { get; }

        /// <summary>
        /// Gets the stream to the file.
        /// </summary>
        Stream Body { get; }

        /// <summary>
        /// Slices a subset of the blob into a another blob.
        /// </summary>
        /// <param name="start">The start of the slicing in bytes.</param>
        /// <param name="end">The end of the slicing in bytes.</param>
        /// <param name="contentType">The mime-type of the new blob.</param>
        /// <returns>A new blob with this blob's subset.</returns>
        [DomName("slice")]
        IBlob Slice(Int32 start = 0, Int32 end = Int32.MaxValue, String contentType = null);

        /// <summary>
        /// Closes the stream to the blob.
        /// </summary>
        [DomName("close")]
        void Close();
    }
}

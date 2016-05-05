namespace AngleSharp.Services.Media
{
    using System;

    /// <summary>
    /// Contains information about an image file.
    /// </summary>
    public interface IImageInfo : IResourceInfo
    {
        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        Int32 Width { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        Int32 Height { get; }
    }
}

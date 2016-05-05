namespace AngleSharp.Services.Media
{
    using System;

    /// <summary>
    /// Contains information about a general object file.
    /// </summary>
    public interface IObjectInfo : IResourceInfo
    {
        /// <summary>
        /// Gets the width of the object.
        /// </summary>
        Int32 Width { get; }

        /// <summary>
        /// Gets the height of the object.
        /// </summary>
        Int32 Height { get; }
    }
}

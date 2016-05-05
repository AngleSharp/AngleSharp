namespace AngleSharp.Services
{
    using System;

    /// <summary>
    /// Defines methods to manipulate the clipboard.
    /// </summary>
    public interface IClipboardService : IService
    {
        /// <summary>
        /// Gets or sets the text that is stored on the clipboard.
        /// </summary>
        String Text { get; set; }
    }
}

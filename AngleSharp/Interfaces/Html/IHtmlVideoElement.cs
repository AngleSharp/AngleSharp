namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the video HTML element.
    /// </summary>
    [DomName("HTMLVideoElement")]
    public interface IHtmlVideoElement : IHtmlMediaElement
    {
        /// <summary>
        /// Gets or sets the displayed width of the video element.
        /// </summary>
        [DomName("width")]
        Int32 Width { get; set; }

        /// <summary>
        /// Gets or sets the displayed height of the video element.
        /// </summary>
        [DomName("height")]
        Int32 Height { get; set; }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        [DomName("videoWidth")]
        Int32 VideoWidth { get; }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        [DomName("videoHeight")]
        Int32 VideoHeight { get; }

        /// <summary>
        /// Gets or sets the URL to a preview image.
        /// </summary>
        [DomName("poster")]
        String Poster { get; }
    }
}

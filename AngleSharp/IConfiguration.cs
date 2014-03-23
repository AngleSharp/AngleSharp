namespace AngleSharp
{
    using AngleSharp.Parser;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents the interface for a general setup of AngleSharp
    /// or a particular AngleSharp request.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets or sets the current scripting mode.
        /// </summary>
        Boolean IsScripting { get; set; }

        /// <summary>
        /// Gets or sets the current CSS mode. Usually CSS stylesheets and inline-
        /// definitions are parsed (can be deactivated here).
        /// </summary>
        Boolean IsStyling { get; set; }

        /// <summary>
        /// Gets or sets the current embedding mode. Normally the document is NOT
        /// embedded. Enabling embedding will emulate the document being rendered
        /// in an iframe.
        /// </summary>
        Boolean IsEmbedded { get; set; }

        /// <summary>
        /// Gets or sets if the quirks mode should be used for HTML / CSS parsing.
        /// </summary>
        Boolean UseQuirksMode { get; set; }

        /// <summary>
        /// Gets or sets the delegate to call in case of an (tolerable) error. If this
        /// is null, then the default behavior is to print these errors on the
        /// debug console (if in debug mode) or to drop the errors completely.
        /// </summary>
        EventHandler<ParseErrorEventArgs> OnError { get; set; }

        /// <summary>
        /// Gets or sets the language (code, e.g. en-US, de-DE) to use.
        /// </summary>
        String Language { get; set; }

        /// <summary>
        /// Gets or sets the culture to use.
        /// </summary>
        CultureInfo Culture { get; set; }
    }
}

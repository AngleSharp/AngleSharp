namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the @import CSS rule.
    /// </summary>
    [DomName("CSSImportRule")]
    public interface ICssImportRule : ICssRule
    {
        /// <summary>
        /// Gets the location of the style sheet to be imported. 
        /// </summary>
        [DomName("href")]
        String Href { get; }

        /// <summary>
        /// Gets a list of media types for which this style sheet may be used.
        /// </summary>
        [DomName("media")]
        [DomPutForwards("mediaText")]
        IMediaList Media { get; }

        /// <summary>
        /// Gets the style sheet referred to by this rule, if it has been loaded. 
        /// </summary>
        [DomName("styleSheet")]
        ICssStyleSheet Sheet { get; }
    }
}

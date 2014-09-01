namespace AngleSharp.DOM.Css
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS background-attachment property.
    /// </summary>
    public interface ICssBackgroundAttachmentProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration with the desired attachment settings.
        /// </summary>
        IEnumerable<BackgroundAttachment> Attachments { get; }
    }
}

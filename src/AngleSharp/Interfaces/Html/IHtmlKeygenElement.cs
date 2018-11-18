namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the keygen HTML element.
    /// </summary>
    [DomName("HTMLKeygenElement")]
    public interface IHtmlKeygenElement : IHtmlFormControlElement
    {
        
        /// <summary>
        /// Gets the type of input control (keygen).
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets or sets the type of encryption used.
        /// </summary>
        [DomName("keytype")]
        String KeyEncryption { get; set; }

        /// <summary>
        /// Gets or sets the challenge attribute.
        /// </summary>
        [DomName("challenge")]
        String Challenge { get; set; }
    }
}

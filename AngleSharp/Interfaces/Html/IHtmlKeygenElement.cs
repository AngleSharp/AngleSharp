namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the keygen HTML element.
    /// </summary>
    [DomName("HTMLKeygenElement")]
    public interface IHtmlKeygenElement : IHtmlElement, IValidation
    {
        /// <summary>
        /// Gets or sets the autofocus HTML attribute, which indicates whether the
        /// control should have input focus when the page loads.
        /// </summary>
        [DomName("autofocus")]
        Boolean Autofocus { get; set; }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DomName("labels")]
        INodeList Labels { get; }

        /// <summary>
        /// Gets or sets if the keygen is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DomName("form")]
        IHtmlFormElement Form { get; }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

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

namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the output HTML element.
    /// </summary>
    [DomName("HTMLOutputElement")]
    public interface IHtmlOutputElement : IHtmlElement, IValidation
    {
        /// <summary>
        /// Gets or sets the IDs of the input elements.
        /// </summary>
        [DomName("htmlFor")]
        ISettableTokenList HtmlFor { get; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        [DomName("defaultValue")]
        String DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DomName("labels")]
        INodeList Labels { get; }

        /// <summary>
        /// Gets the type of input control (output).
        /// </summary>
        [DomName("type")]
        String Type { get; }

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
    }
}

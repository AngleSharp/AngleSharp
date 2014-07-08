namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the output HTML element.
    /// </summary>
    [DomName("HTMLOutputElement")]
    public interface IHtmlOutputElement : IHtmlElement
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

        /// <summary>
        /// Gets a value if the current element validates.
        /// </summary>
        [DomName("willValidate")]
        Boolean WillValidate { get; }

        /// <summary>
        /// Gets the current validation state of the current element.
        /// </summary>
        [DomName("validity")]
        IValidityState Validity { get; }

        /// <summary>
        /// Gets the current validation message.
        /// </summary>
        [DomName("validationMessage")]
        String ValidationMessage { get; }

        /// <summary>
        /// Checks the validity of the current element.
        /// </summary>
        /// <returns>True if the object is valid, otherwise false.</returns>
        [DomName("checkValidity")]
        Boolean CheckValidity();

        /// <summary>
        /// Sets a custom validation error. If this is not the empty string,
        /// then the element is suffering from a custom validation error.
        /// </summary>
        /// <param name="error">The error message to use.</param>
        [DomName("setCustomValidity")]
        void SetCustomValidity(String error);
    }
}

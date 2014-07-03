namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the fieldset HTML element.
    /// </summary>
    [DomName("HTMLFieldSetElement")]
    public interface IHtmlFieldSetElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets if the element is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean Disabled { get; set; }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DomName("form")]
        IHtmlFormElement Form { get; }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets the type of input control (fieldset).
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets the elements belonging to this field set.
        /// </summary>
        [DomName("elements")]
        IHtmlFormControlsCollection Elements { get; }

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

namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Implemented by elements that can be validated.
    /// </summary>
    public interface IValidation
    {
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

namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the form HTML element.
    /// </summary>
    [DomName("HTMLFormElement")]
    public interface IHtmlFormElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the character encodings that are to be used for the submission.
        /// </summary>
        [DomName("acceptCharset")]
        String AcceptCharset { get; set; }

        /// <summary>
        /// Gets or sets the form's name within the forms collection.
        /// </summary>
        [DomName("action")]
        String Action { get; set; }

        /// <summary>
        /// Gets or sets if autocomplete is turned on or off.
        /// </summary>
        [DomName("autocomplete")]
        String Autocomplete { get; set; }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        [DomName("enctype")]
        String Enctype { get; set; }

        /// <summary>
        /// Gets or sets the encoding to use for sending the form.
        /// </summary>
        [DomName("encoding")]
        String Encoding { get; set; }

        /// <summary>
        /// Gets or sets the method to use for transmitting the form.
        /// </summary>
        [DomName("method")]
        String Method { get; set; }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets or sets the indicator that the form is not to be validated during submission.
        /// </summary>
        [DomName("noValidate")]
        Boolean NoValidate { get; set; }

        /// <summary>
        /// Gets or sets the target name of the response to the request.
        /// </summary>
        [DomName("target")]
        String Target { get; set; }

        /// <summary>
        /// Gets the number of elements in the Elements collection.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets all the form controls belonging to this form element.
        /// </summary>
        [DomName("elements")]
        IHtmlFormControlsCollection Elements { get; }

        /// <summary>
        /// Submits the form element from the form element itself.
        /// </summary>
        [DomName("submit")]
        Task<IDocument> Submit();

        /// <summary>
        /// Resets the form to the previous (default) state.
        /// </summary>
        [DomName("reset")]
        void Reset();

        /// <summary>
        /// Checks if the form is valid, i.e. if all fields fulfill their requirements.
        /// </summary>
        /// <returns>True if the form is valid, otherwise false.</returns>
        [DomName("checkValidity")]
        Boolean CheckValidity();

        /// <summary>
        /// Reports the current validity state after checking the current state
        /// interactively the constraints of the form element.
        /// </summary>
        /// <returns>True if the form element is valid, otherwise false.</returns>
        [DomName("reportValidity")]
        Boolean ReportValidity();

        /// <summary>
        /// Gets the form element at the specified index.
        /// </summary>
        /// <param name="index">The index in the elements collection.</param>
        /// <returns>The element or null.</returns>
        [DomAccessor(Accessors.Getter)]
        IElement this[Int32 index] { get; }

        /// <summary>
        /// Gets the form element(s) with the specified name.
        /// </summary>
        /// <param name="name">The name or id of the element.</param>
        /// <returns>A collection with elements, an element or null.</returns>
        [DomAccessor(Accessors.Getter)]
        IElement this[String name] { get; }

        /// <summary>
        /// Requests the input fields to be automatically filled with previous entries.
        /// </summary>
        [DomName("requestAutocomplete")]
        void RequestAutocomplete();
    }
}

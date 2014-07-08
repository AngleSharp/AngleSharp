namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the input HTML element.
    /// </summary>
    [DomName("HTMLInputElement")]
    public interface IHtmlInputElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the autofocus HTML attribute, which indicates whether the
        /// control should have input focus when the page loads.
        /// </summary>
        [DomName("autofocus")]
        Boolean Autofocus { get; set; }

        /// <summary>
        /// Gets or sets if the textarea is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets the associated HTML form element.
        /// </summary>
        [DomName("form")]
        IHtmlFormElement Form { get; }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DomName("labels")]
        INodeList Labels { get; }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets or sets the type of input control.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets or sets if the field is required.
        /// </summary>
        [DomName("required")]
        Boolean IsRequired { get; set; }

        /// <summary>
        /// Gets or sets if the field is read-only.
        /// </summary>
        [DomName("readOnly")]
        Boolean IsReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        [DomName("alt")]
        String AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets the URI of a resource that processes information submitted by the button.
        /// If specified, this attribute overrides the action attribute of the form element that owns this element.
        /// </summary>
        [DomName("formAction")]
        String FormAction { get; set; }

        /// <summary>
        /// Gets or sets the type of content that is used to submit the form to the server. If specified, this
        /// attribute overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        [DomName("formEncType")]
        String FormEncType { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method that the browser uses to submit the form. If specified, this attribute
        /// overrides the method attribute of the form element that owns this element.
        /// </summary>
        [DomName("formMethod")]
        String FormMethod { get; set; }

        /// <summary>
        /// Gets or sets that the form is not to be validated when it is submitted. If specified, this attribute
        /// overrides the enctype attribute of the form element that owns this element.
        /// </summary>
        [DomName("formNoValidate")]
        Boolean FormNoValidate { get; set; }

        /// <summary>
        /// Gets or sets A name or keyword indicating where to display the response that is received after submitting
        /// the form. If specified, this attribute overrides the target attribute of the form element that owns this element.
        /// </summary>
        [DomName("formTarget")]
        String FormTarget { get; set; }

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
        /// Gets or sets the display size of the element.
        /// </summary>
        [DomName("size")]
        Int32 Size { get; set; }

        /// <summary>
        /// Gets or sets the multiple HTML attribute, whichindicates whether
        /// multiple items can be selected.
        /// </summary>
        [DomName("multiple")]
        Boolean IsMultiple { get; set; }

        /// <summary>
        /// Gets or sets the maxlength HTML attribute, indicating
        /// the maximum number of characters the user can enter.
        /// This constraint is evaluated only when the value changes.
        /// </summary>
        [DomName("maxLength")]
        Int32 MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the placeholder HTML attribute, containing a hint to
        /// the user about what to enter in the control.
        /// </summary>
        [DomName("placeholder")]
        String Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the display width of the input element.
        /// </summary>
        [DomName("width")]
        Int32 DisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the display height of the input element.
        /// </summary>
        [DomName("height")]
        Int32 DisplayHeight { get; set; }

        /// <summary>
        /// Gets the direction in which selection occurred. This is "forward" if
        /// selection was performed in the start-to-end direction of the current
        /// locale, or "backward" for the opposite direction.
        /// </summary>
        [DomName("selectionDirection")]
        String SelectionDirection { get; }

        /// <summary>
        /// Gets or sets the directionality of the form element.
        /// </summary>
        [DomName("dirName")]
        String DirectionName { get; set; }

        /// <summary>
        /// Gets or sets the index of the beginning of selected text.
        /// If no text is selected, contains the index of the character
        /// that follows the input cursor. On being set, the control behaves
        /// as if setSelectionRange() had been called with this as the first
        /// argument, and selectionEnd as the second argument.
        /// </summary>
        [DomName("selectionStart")]
        Int32 SelectionStart { get; set; }

        /// <summary>
        /// Gets or sets the index of the end of selected text. If no text
        /// is selected, contains the index of the character that follows
        /// the input cursor. On being set, the control behaves as if
        /// setSelectionRange() had been called with this as the second
        /// argument, and selectionStart as the first argument.
        /// </summary>
        [DomName("selectionEnd")]
        Int32 SelectionEnd { get; set; }

        /// <summary>
        /// Selects the contents of the textarea.
        /// </summary>
        [DomName("select")]
        void SelectAll();

        /// <summary>
        /// Selects a range of text, and sets selectionStart and selectionEnd.
        /// If either argument is greater than the length of the value, it is treated
        /// as pointing to the end of the value. If end is less than start, then
        /// both are treated as the value of end.
        /// </summary>
        /// <param name="selectionStart">The start of the selection.</param>
        /// <param name="selectionEnd">The end of the selection.</param>
        /// <param name="selectionDirection">Optional: The direction of the selection.</param>
        [DomName("setSelectionRange")]
        void Select(Int32 selectionStart, Int32 selectionEnd, String selectionDirection = null);

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

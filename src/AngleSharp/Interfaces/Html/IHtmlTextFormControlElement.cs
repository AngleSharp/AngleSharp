namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the base class for all HTML text form controls.
    /// </summary>
    public interface IHtmlTextFormControlElement : IHtmlFormControlElement
    {

        /// <summary>
        /// Gets or sets if the value has been modified.
        /// </summary>
        Boolean IsDirty { get; set; }

        /// <summary>
        /// Gets or sets the directionality of the form element.
        /// </summary>
        [DomName("dirName")]
        String DirectionName { get; set; }

        /// <summary>
        /// Gets or sets the maxlength HTML attribute, indicating
        /// the maximum number of characters the user can enter.
        /// This constraint is evaluated only when the value changes.
        /// </summary>
        Int32 MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the maxlength HTML attribute, indicating the maximum
        /// number of characters the user can enter. This constraint is 
        /// evaluated only when the value changes.
        /// </summary>
        [DomName("maxLength")]
        Int32 MinLength { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        [DomName("defaultValue")]
        String DefaultValue { get; set; }

        /// <summary>
        /// Gets if the input field has a value (via attribute or directly).
        /// </summary>
        Boolean HasValue { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }

        /// <summary>
        /// Gets or sets the placeholder HTML attribute, containing a hint to
        /// the user about what to enter in the control.
        /// </summary>
        [DomName("placeholder")]
        String Placeholder { get; set; }

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
        /// Gets or sets the index of the beginning of selected text. If no 
        /// text is selected, contains the index of the character that 
        /// follows the input cursor. On being set, the control behaves as if
        /// setSelectionRange() had been called with this as the first argument,
        /// and selectionEnd as the second argument.
        /// </summary>
        [DomName("selectionStart")]
        Int32 SelectionStart { get; set; }

        /// <summary>
        /// Gets or sets the index of the end of selected text. If no text
        /// is selected, contains the index of the character that follows the
        /// input cursor. On being set, the control behaves as if 
        /// setSelectionRange() had been called with this as the second 
        /// argument, and selectionStart as the first argument.
        /// </summary>
        [DomName("selectionEnd")]
        Int32 SelectionEnd { get; set; }

        /// <summary>
        /// Gets the direction in which selection occurred. This is "forward" if
        /// selection was performed in the start-to-end direction of the current
        /// locale, or "backward" for the opposite direction.
        /// </summary>
        [DomName("selectionDirection")]
        String SelectionDirection { get; }

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
        /// Selects the contents of the control.
        /// </summary>
        [DomName("select")]
        void SelectAll();

    }
}

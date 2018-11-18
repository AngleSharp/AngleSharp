namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the textarea HTML element.
    /// </summary>
    [DomName("HTMLTextAreaElement")]
    public interface IHtmlTextAreaElement : IHtmlTextFormControlElement
    {
        
        /// <summary>
        /// Gets the type (textarea).
        /// </summary>
        [DomName("type")]
        String Type { get; }
        
        /// <summary>
        /// Gets or sets the wrap HTML attribute, indicating how the control wraps text.
        /// </summary>
        [DomName("wrap")]
        String Wrap { get; set; }

        /// <summary>
        /// Gets the codepoint length of the control's value.
        /// </summary>
        [DomName("textLength")]
        Int32 TextLength { get; }

        /// <summary>
        /// Gets or sets the rows HTML attribute, indicating
        /// the number of visible text lines for the control.
        /// </summary>
        [DomName("rows")]
        Int32 Rows { get; set; }

        /// <summary>
        /// Gets or sets the cols HTML attribute, indicating
        /// the visible width of the text area.
        /// </summary>
        [DomName("cols")]
        Int32 Columns { get; set; }
        
    }
}

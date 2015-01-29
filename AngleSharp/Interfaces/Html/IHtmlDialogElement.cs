namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Repesents the dialog HTML element.
    /// </summary>
    [DomName("HTMLDialogElement")]
    public interface IHtmlDialogElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets if the dialog is open.
        /// </summary>
        [DomName("open")]
        Boolean Open { get; set; }

        /// <summary>
        /// Gets or sets the return value of the dialog.
        /// </summary>
        [DomName("returnValue")]
        String ReturnValue { get; set; }
        
        /// <summary>
        /// Shows the dialog, optionally anchored to the element.
        /// </summary>
        /// <param name="anchor">The element, where the dialog is anchored.</param>
        [DomName("show")]
        void Show(IElement anchor = null);

        /// <summary>
        /// Shows the dialog modally, optionally anchored to the element.
        /// </summary>
        /// <param name="anchor">The element, where the dialog is anchored.</param>
        [DomName("showModal")]
        void ShowModal(IElement anchor = null);

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="returnValue">The return value to set.</param>
        [DomName("close")]
        void Close(String returnValue = null);
    }
}

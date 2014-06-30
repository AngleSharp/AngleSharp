namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Reprsents the td HTML element.
    /// </summary>
    [DomName("HTMLTableCellElement")]
    public interface IHtmlTableCellElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the number of columns spanned by cell. 
        /// </summary>
        [DomName("colSpan")]
        UInt32 ColSpan { get; set; }

        /// <summary>
        /// Gets or sets the number of rows spanned by cell. 
        /// </summary>
        [DomName("rowSpan")]
        UInt32 RowSpan { get; set; }

        /// <summary>
        /// Gets or sets the list of id attribute values for header cells. 
        /// </summary>
        [DomName("headers")]
        ISettableTokenList Headers { get; }

        /// <summary>
        /// Gets the index of this cell in the row, starting from 0.
        /// This index is in document tree order and not display order.
        /// </summary>
        [DomName("cellIndex")]
        Int32 Index { get; }
    }
}

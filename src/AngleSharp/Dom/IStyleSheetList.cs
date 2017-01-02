namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of stylesheet elements.
    /// </summary>
    [DomName("StyleSheetList")]
    public interface IStyleSheetList : IEnumerable<IStyleSheet>
    {
        /// <summary>
        /// Gets the stylesheet at the specified index. If index is greater
        /// than or equal to the number of style sheets in the list, this
        /// returns null.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The stylesheet.</returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        IStyleSheet this[Int32 index] { get; }
        
        /// <summary>
        /// Gets the number of elements in the list of stylesheets.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }
    }
}

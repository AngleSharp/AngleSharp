namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Provides the abstraction of an ordered collection of CSS values.
    /// </summary>
    [DomName("CSSValueList")]
    public interface ICssValueList : ICssValue
    {
        /// <summary>
        /// Gets the number of CSSValues in the list. The range of valid
        /// values of the indices is 0 to length - 1 inclusive.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the value at the specified position.
        /// </summary>
        /// <param name="index">Index into the collection.</param>
        /// <returns>
        /// The CSSValue at the index position in the list, or null if that
        /// is not a valid index.
        /// </returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        ICssValue this[Int32 index] { get; }
    }
}

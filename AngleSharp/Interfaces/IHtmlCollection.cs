namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// HTMLCollection is an interface representing a generic collection (array)
    /// of elements (in document order) and offers methods and properties for selecting
    /// from the list.
    /// </summary>
    [DOM("HTMLCollection")]
    public interface IHtmlCollection : IEnumerable<Element>
    {
        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        [DOM("length")]
        Int32 Length { get; }
  
        /// <summary>
        /// Gets the specific node at the given zero-based index into the list.
        /// </summary>
        /// <param name="index">The zero-based index.</param>
        /// <returns>Returns null if the index is out of range.</returns>
        [DOM("item")]
        Element this[Int32 index] { get; }
  
        /// <summary>
        /// Gets the specific node whose ID or, as a fallback, name matches the
        /// string specified by name. Matching by name is only done as a last resort,
        /// only in HTML, and only if the referenced element supports the name attribute.
        /// </summary>
        /// <param name="name">The id or name to match.</param>
        /// <returns>Returns null if no node exists by the given name.</returns>
        [DOM("namedItem")]
        Element this[String name] { get; }
    }
}

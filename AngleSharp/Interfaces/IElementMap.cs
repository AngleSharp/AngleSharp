namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a set of name-element mappings.
    /// </summary>
    [DomName("DOMElementMap")]
    public interface IElementMap : IEnumerable<KeyValuePair<String, IElement>>
    {
        /// <summary>
        /// Gets or sets an element in the dictionary.
        /// </summary>
        /// <param name="name">The name of the element to get or set.</param>
        /// <returns>The element with the associated name.</returns>
        [DomAccessor(Accessors.Getter | Accessors.Setter)]
        IElement this[String name] { get; set; }

        /// <summary>
        /// Deletes the element with the given name from the map.
        /// </summary>
        /// <param name="name">The name of the element to remove.</param>
        [DomAccessor(Accessors.Deleter)]
        void Remove(String name);
    }
}

namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a string list.
    /// </summary>
    [DomName("StringList")]
    public interface IStringList : IEnumerable<String>
    {
        [DomName("item")]
        String this[Int32 index] { get; }
        
        /// <summary>
        /// Gets the number of entries.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }
        
        /// <summary>
        /// Returns a boolean indicating if the specified entry is available.
        /// </summary>
        /// <param name="entry">The entry that will be looked for.</param>
        /// <returns>True if the element is available, otherwise false.</returns>
        [DomName("contains")]
        Boolean Contains(String entry);
    }
}

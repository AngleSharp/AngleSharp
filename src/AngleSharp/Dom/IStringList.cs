namespace AngleSharp.Dom;

using AngleSharp.Attributes;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents a string list.
/// </summary>
[DomName("DOMStringList")]
public interface IStringList :
#if NET8_0_OR_GREATER
    IReadOnlyList<String>
#else
    IEnumerable<String>
#endif
{
    /// <summary>
    /// Gets the value at the specified index.
    /// </summary>
    /// <param name="index">The index of the value.</param>
    /// <returns>The string value at the given index.</returns>
    [DomName("item")]
    [DomAccessor(Accessors.Getter)]
#if NET8_0_OR_GREATER
    // Declared on every target: code built against the netstandard2.0 build calls this
    // member, not the IReadOnlyList<T> one, so it has to exist here too. Implementers still
    // provide the IReadOnlyList<T> indexer (a public this[Int32] covers both).
    new String this[Int32 index] => ((IReadOnlyList<String>)this)[index];

    abstract String IReadOnlyList<String>.this[Int32 index] { get; }
#else
    String this[Int32 index] { get; }
#endif

    /// <summary>
    /// Gets the number of entries.
    /// </summary>
    [DomName("length")]
    Int32 Length { get; }

    /// <summary>
    /// Returns a boolean indicating if the specified entry is available.
    /// </summary>
    /// <param name="entry">The entry that will be looked for.</param>
    /// <returns>
    /// True if the element is available, otherwise false.
    /// </returns>
    [DomName("contains")]
    Boolean Contains(String entry);

#if NET8_0_OR_GREATER
    Int32 IReadOnlyCollection<String>.Count => Length;
#endif
}

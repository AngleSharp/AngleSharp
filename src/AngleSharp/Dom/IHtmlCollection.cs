namespace AngleSharp.Dom;

using AngleSharp.Attributes;
using System;
using System.Collections.Generic;

/// <summary>
/// HTMLCollection is an interface representing a generic collection
/// (array) of elements (in document order) and offers methods and
/// properties for selecting from the list.
/// </summary>
[DomName("HTMLCollection")]
public interface IHtmlCollection<T> :
#if NET8_0_OR_GREATER
    IReadOnlyList<T>
#else
    IEnumerable<T>
#endif

    where T : IElement
{
    /// <summary>
    /// Gets the number of items in the collection.
    /// </summary>
    [DomName("length")]
    Int32 Length { get; }

    /// <summary>
    /// Gets the specific node at the given zero-based index into the list.
    /// </summary>
    /// <param name="index">The zero-based index.</param>
    /// <returns>Returns the element at the specified index.</returns>
    [DomName("item")]
    [DomAccessor(Accessors.Getter)]
#if NET8_0_OR_GREATER
    // Declared on every target: code built against the netstandard2.0 build calls this
    // member, not the IReadOnlyList<T> one, so it has to exist here too. Implementers still
    // provide the IReadOnlyList<T> indexer (a public this[Int32] covers both).
    new T this[Int32 index] => ((IReadOnlyList<T>)this)[index];

    abstract T IReadOnlyList<T>.this[Int32 index] { get; }
#else
    T this[Int32 index] { get; }
#endif

    /// <summary>
    /// Gets the specific node whose ID or, as a fallback, name matches the
    /// string specified by name. Matching by name is only done as a last
    /// resort, only in HTML, and only if the referenced element supports 
    /// the name attribute.
    /// </summary>
    /// <param name="id">The id or name to match.</param>
    /// <returns>Returns the element with the specified name.</returns>
    [DomName("namedItem")]
    [DomAccessor(Accessors.Getter)]
    T? this[String id] { get; }

#if NET8_0_OR_GREATER
    Int32 IReadOnlyCollection<T>.Count => Length;
#endif
}

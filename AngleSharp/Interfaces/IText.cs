namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
	using System;

    /// <summary>
    /// The Text interface represents the textual content of Element or Attr.
    /// If an element has no markup within its content, it has a single child
    /// implementing Text that contains the element's text.  However, if the
    /// element contains markup, it is parsed into information items and Text
    /// nodes that form its children.
    /// </summary>
	[DomName("Text")]
	public interface IText : ICharacterData
	{
        /// <summary>
        /// Breaks the node into two nodes at a specified offset.
        /// </summary>
        /// <param name="offset">
        /// The point where the Node should be split.
        /// </param>
        /// <returns>
        /// The freshly created Text element with the rest of the content.
        /// </returns>
		[DomName("splitText")]
		IText Split(Int32 offset);

        /// <summary>
        /// Gets a string containing the text of all Text nodes logically
        /// adjacent to this Node, concatenated in document order.
        /// </summary>
		[DomName("wholeText")]
		String Text { get; }
	}
}
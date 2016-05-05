namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Defines the ParentNode interface that is implemented by possible
    /// parents.
    /// </summary>
    [DomName("ParentNode")]
    [DomNoInterfaceObject]
    public interface IParentNode
    {
        /// <summary>
        /// Gets the child elements.
        /// </summary>
        [DomName("children")]
        IHtmlCollection<IElement> Children { get; }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        [DomName("firstElementChild")]
        IElement FirstElementChild { get; }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        [DomName("lastElementChild")]
        IElement LastElementChild { get; }

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        [DomName("childElementCount")]
        Int32 ChildElementCount { get; }

        /// <summary>
        /// Appends nodes to current document.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        [DomName("append")]
        void Append(params INode[] nodes);

        /// <summary>
        /// Prepends nodes to the current document.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        [DomName("prepend")]
        void Prepend(params INode[] nodes);

        /// <summary>
        /// Returns the first element within the document (using depth-first
        /// pre-order traversal of the document's nodes) that matches the
        /// specified group of selectors.
        /// </summary>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>The found element.</returns>
        [DomName("querySelector")]
        IElement QuerySelector(String selectors);

        /// <summary>
        /// Returns a list of the elements within the document (using
        /// depth-first pre-order traversal of the document's nodes) that match
        /// the specified group of selectors.
        /// </summary>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>A non-live NodeList of element objects.</returns>
        [DomName("querySelectorAll")]
        IHtmlCollection<IElement> QuerySelectorAll(String selectors);
    }
}

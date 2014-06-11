namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Document implements ParentNode;
    /// DocumentFragment implements ParentNode;
    /// Element implements ParentNode;
    /// </summary>
    [DomName("ParentNode")]
    interface IParentNode
    {
        [DomName("children")]
        IHtmlCollection Children { get; }

        [DomName("firstElementChild")]
        IElement FirstElementChild { get; }

        [DomName("lastElementChild")]
        IElement LastElementChild { get; }

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
        /// Returns a list of the elements within the document (using depth-first
        /// pre-order traversal of the document's nodes) that match the specified
        /// group of selectors.
        /// </summary>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>A non-live NodeList of element objects.</returns>
        [DomName("querySelectorAll")]
        INodeList QuerySelectorAll(String selectors);
    }
}

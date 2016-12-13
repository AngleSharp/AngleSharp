namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Extensions to ParentNode nodes that are not Element nodes.
    /// </summary>
    [DomName("NonElementParentNode")]
    [DomNoInterfaceObject]
    public interface INonElementParentNode
    {
        /// <summary>
        /// Returns the Element whose ID is given by elementId. If no such
        /// element exists, returns null. The behavior is not defined if
        /// more than one element have this ID. 
        /// </summary>
        /// <param name="elementId">
        /// A case-sensitive string representing the unique ID of the element
        /// being sought.
        /// </param>
        /// <returns>The matching element.</returns>
        [DomName("getElementById")]
        IElement GetElementById(String elementId);
    }
}

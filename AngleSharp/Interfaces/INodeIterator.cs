namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// The NodeIterator interface represents an iterator over the members of a
    /// list of the nodes in a subtree of the DOM. The nodes will be returned in
    /// document order.
    /// </summary>
    [DomName("NodeIterator")]
    public interface INodeIterator
    {
        [DomName("root")]
        INode Root { get; }

        [DomName("referenceNode")]
        INode Reference { get; }

        [DomName("pointerBeforeReferenceNode")]
        Boolean IsBeforeReference { get; }

        [DomName("whatToShow")]
        FilterSetting Settings { get; }

        [DomName("filter")]
        NodeFilter Filter { get; }

        [DomName("nextNode")]
        INode Next();

        [DomName("previousNode")]
        INode Previous();

        [DomName("detach")]
        void Detach();
    }
}
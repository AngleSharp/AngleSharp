namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// MutationRecord defines an interface that will be passed
    /// to the observer's callback.
    /// </summary>
    [DomName("MutationRecord")]
    interface IMutationRecord
    {
        /// <summary>
        /// Gets attributes if the mutation was an attribute mutation,
        /// characterData if it was a mutation to a CharacterData node,
        /// and childList if it was a mutation to the tree of nodes.
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets the node the mutation affected, depending on the type. For
        /// attributes, it is the element whose attribute changed. For characterData,
        /// it is the CharacterData node. For childList, it is the node whose
        /// children changed.
        /// </summary>
        [DomName("target")]
        INode Target { get; }

        /// <summary>
        /// Gets the nodes added, or null.
        /// </summary>
        [DomName("addedNodes")]
        INodeList AddedNodes { get; }

        /// <summary>
        /// Gets the nodes removed, or null.
        /// </summary>
        [DomName("removedNodes")]
        INodeList RemovedNodes { get; }

        /// <summary>
        /// Gets the previous sibling of the added or removed nodes, or null.
        /// </summary>
        [DomName("previousSibling")]
        INode PreviousSibling { get; }

        /// <summary>
        /// Gets the next sibling of the added or removed nodes, or null.
        /// </summary>
        [DomName("nextSibling")]
        INode NextSibling { get; }

        /// <summary>
        /// Gets the local name of the changed attribute, or null.
        /// </summary>
        [DomName("attributeName")]
        String AttributeName { get; }

        /// <summary>
        /// Gets the namespace of the changed attribute, or null.
        /// </summary>
        [DomName("attributeNamespace")]
        String AttributeNamespace { get; }

        /// <summary>
        /// Gets a string depending on the type. For attributes, it is the value
        /// of the changed attribute before the change. For characterData, it is
        /// the data of the changed node before the change. For childList, it is
        /// null.
        /// </summary>
        [DomName("oldValue")]
        String OldValue { get; }
    }
}

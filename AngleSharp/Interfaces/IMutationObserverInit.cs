namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Defines a set of options to use with the mutation observer.
    /// </summary>
    [DOM("MutationObserverInit")]
    interface IMutationObserverInit
    {
        /// <summary>
        /// Gets or sets if additions and removals of the target node's child
        /// elements (including text nodes) are to be observed.
        /// </summary>
        [DOM("childList")]
        Boolean ChildList { get; set; }

        /// <summary>
        /// Gets or sets if mutations to target's attributes are to be observed.
        /// </summary>
        [DOM("attributes")]
        Boolean Attributes { get; set; }

        /// <summary>
        /// Gets or sets if mutations to target's data are to be observed.
        /// </summary>
        [DOM("characterData")]
        Boolean CharacterData { get; set; }

        /// <summary>
        /// Gets or sets if mutations to not just target, but also
        /// target's descendants are to be observed.
        /// </summary>
        [DOM("subtree")]
        Boolean Subtree { get; set; }

        /// <summary>
        /// Gets or sets if attributes is set to true and target's attribute
        /// value before the mutation needs to be recorded.
        /// </summary>
        [DOM("attributeOldValue")]
        Boolean AttributeOldValue { get; set; }

        /// <summary>
        /// Gets or sets if characterData is set to true and target's
        /// data before the mutation needs to be recorded.
        /// </summary>
        [DOM("characterDataOldValue")]
        Boolean CharacterDataOldValue { get; set; }

        /// <summary>
        /// Gets or sets if not all attribute mutations need to be observed.
        /// </summary>
        [DOM("attributeFilter")]
        Boolean AttributeFilter { get; set; }
    }
}

namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a set of options to use with the mutation observer.
    /// </summary>
    [DomName("MutationObserverInit")]
    public interface IMutationObserverInit
    {
        /// <summary>
        /// Gets or sets if additions and removals of the target node's child
        /// elements (including text nodes) are to be observed.
        /// </summary>
        [DomName("childList")]
        Boolean ObserveTargetChildNodes { get; set; }

        /// <summary>
        /// Gets or sets if mutations to not just target, but also
        /// target's descendants are to be observed.
        /// </summary>
        [DomName("subtree")]
        Boolean ObserveTargetDescendents { get; set; }

        /// <summary>
        /// Gets or sets if mutations to target's attributes are to be observed.
        /// </summary>
        [DomName("attributes")]
        Boolean? ObserveTargetAttributes { get; set; }

        /// <summary>
        /// Gets or sets if mutations to target's data are to be observed.
        /// </summary>
        [DomName("characterData")]
        Boolean? ObserveTargetData { get; set; }

        /// <summary>
        /// Gets or sets if attributes is set to true and target's attribute
        /// value before the mutation needs to be recorded.
        /// </summary>
        [DomName("attributeOldValue")]
        Boolean? StorePreviousAttributeValue { get; set; }

        /// <summary>
        /// Gets or sets if characterData is set to true and target's
        /// data before the mutation needs to be recorded.
        /// </summary>
        [DomName("characterDataOldValue")]
        Boolean? StorePreviousDataValue { get; set; }

        /// <summary>
        /// Gets or sets if the attributes to observe. If this is not set,
        /// then all attributes are being observed.
        /// </summary>
        [DomName("attributeFilter")]
        IEnumerable<String> AttributeFilters { get; set; }
    }
}

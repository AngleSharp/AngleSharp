namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a set of options to use with the mutation observer.
    /// https://developer.mozilla.org/en-US/docs/Web/API/MutationObserver#MutationObserverInit
    /// </summary>
    [DomNoInterfaceObject]
    [DomName("MutationObserverInit")]
    public sealed class MutationObserverInit
    {
        /// <summary>
        /// Creates a new mutation observer configuration.
        /// </summary>
        public MutationObserverInit()
        {
            IsObservingChildNodes = false;
            IsObservingSubtree = false;
        }

        /// <summary>
        /// Gets or sets if additions and removals of the target node's child
        /// elements (including text nodes) are to be observed.
        /// </summary>
        [DomName("childList")]
        public Boolean IsObservingChildNodes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if mutations to not just target, but also target's
        /// descendants are to be observed.
        /// </summary>
        [DomName("subtree")]
        public Boolean IsObservingSubtree
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if mutations to target's attributes are to be
        /// observed.
        /// </summary>
        [DomName("attributes")]
        public Boolean? IsObservingAttributes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if mutations to target's data are to be observed.
        /// </summary>
        [DomName("characterData")]
        public Boolean? IsObservingCharacterData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if attributes is set to true and target's attribute
        /// value before the mutation needs to be recorded.
        /// </summary>
        [DomName("attributeOldValue")]
        public Boolean? IsExaminingOldAttributeValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if characterData is set to true and target's data
        /// before the mutation needs to be recorded.
        /// </summary>
        [DomName("characterDataOldValue")]
        public Boolean? IsExaminingOldCharacterData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the attributes to observe. If this is not set, then
        /// all attributes are being observed.
        /// </summary>
        [DomName("attributeFilter")]
        public IEnumerable<String> AttributeFilters
        {
            get;
            set;
        }
    }
}

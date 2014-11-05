namespace AngleSharp.DOM.Xml
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The object representation of an XMLElement.
    /// </summary>
    sealed class XmlElement : Element
    {        
        #region ctor

        /// <summary>
        /// Creates a new XML element.
        /// </summary>
        internal XmlElement(String name)
            : base(name)
        {
            NamespaceUri = Namespaces.XmlUri;
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        internal static XmlElement Create(String tagName)
        {
            return new XmlElement(tagName);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets what the id attribute is.
        /// </summary>
        internal String IdAttribute
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = Create(NodeName);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            node.IdAttribute = IdAttribute;
            return node;
        }

        #endregion
    }
}

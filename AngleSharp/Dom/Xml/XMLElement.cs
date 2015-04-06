namespace AngleSharp.Dom.Xml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The object representation of an XMLElement.
    /// </summary>
    sealed class XmlElement : Element
    {        
        #region ctor

        /// <summary>
        /// Creates a new XML element.
        /// </summary>
        public XmlElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix, Namespaces.XmlUri)
        {
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
            var node = new XmlElement(Owner, LocalName, Prefix);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            node.IdAttribute = IdAttribute;
            return node;
        }

        #endregion
    }
}

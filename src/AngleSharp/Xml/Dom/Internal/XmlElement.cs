namespace AngleSharp.Xml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The object representation of an XMLElement.
    /// </summary>
    sealed class XmlElement : Element
    {        
        #region ctor

        public XmlElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix, null)
        {
        }

        #endregion

        #region Properties

        internal String IdAttribute
        {
            get;
            set;
        }

        #endregion

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var node = new XmlElement(owner, LocalName, Prefix);
            CloneElement(node, owner, deep);
            node.IdAttribute = IdAttribute;
            return node;
        }

        #endregion
    }
}

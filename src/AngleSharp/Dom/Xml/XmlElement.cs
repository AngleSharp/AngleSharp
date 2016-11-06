namespace AngleSharp.Dom.Xml
{
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

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var node = new XmlElement(Owner, LocalName, Prefix);
            CloneElement(node, deep);
            node.IdAttribute = IdAttribute;
            return node;
        }

        #endregion
    }
}

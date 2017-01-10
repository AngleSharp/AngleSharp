namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    class SvgElement : Element, ISvgElement
    {
        #region ctor
        
        public SvgElement(Document owner, String name, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, NamespaceNames.SvgUri, flags | NodeFlags.SvgMember)
        {
        }

        #endregion

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var factory = Context.GetFactory<IElementFactory<Document, SvgElement>>();
            var node = factory.Create(Owner, LocalName, Prefix);
            CloneElement(node, deep);
            return node;
        }

        #endregion
    }
}

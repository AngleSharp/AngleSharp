namespace AngleSharp.Mathml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents an element of the MathML DOM.
    /// </summary>
    class MathElement : Element
    {
        #region ctor

        public MathElement(Document owner, String name, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, NamespaceNames.MathMlUri, flags | NodeFlags.MathMember)
        {
        }

        #endregion

        #region Methods

        public override INode Clone(Boolean deep = true)
        {
            var factory = Context.GetFactory<IElementFactory<Document, MathElement>>();
            var node = factory.Create(Owner, LocalName, Prefix);
            CloneElement(node, deep);
            return node;
        }

        #endregion
    }
}

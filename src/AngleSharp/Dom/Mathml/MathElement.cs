namespace AngleSharp.Dom.Mathml
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services;
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
            var factory = Owner.Options.GetFactory<IElementFactory<MathElement>>();
            var node = factory.Create(Owner, LocalName, Prefix);
            CloneElement(node, deep);
            return node;
        }

        #endregion
    }
}

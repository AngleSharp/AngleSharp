namespace AngleSharp.Dom.Mathml
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an element of the MathML DOM.
    /// </summary>
    internal class MathElement : Element
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
            var node = Factory.MathElements.Create(Owner, LocalName, Prefix);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion
    }
}

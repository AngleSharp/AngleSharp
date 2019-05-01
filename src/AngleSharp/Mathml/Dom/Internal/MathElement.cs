namespace AngleSharp.Mathml.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents an element of the MathML DOM.
    /// </summary>
    public class MathElement : Element
    {
        #region ctor

        /// <inheritdoc />
        public MathElement(Document owner, String name, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, NamespaceNames.MathMlUri, flags | NodeFlags.MathMember)
        {
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override IElement ParseSubtree(String html) => this.ParseHtmlSubtree(html);

        /// <inheritdoc />
        public override Node Clone(Document owner, Boolean deep)
        {
            var factory = Context.GetFactory<IElementFactory<Document, MathElement>>();
            var node = factory.Create(owner, LocalName, Prefix);
            CloneElement(node, owner, deep);
            return node;
        }

        #endregion
    }
}

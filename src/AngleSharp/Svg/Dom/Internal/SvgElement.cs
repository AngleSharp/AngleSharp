namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    public class SvgElement : Element, ISvgElement
    {
        #region ctor

        /// <inheritdoc />
        public SvgElement(Document owner, String name, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, NamespaceNames.SvgUri, flags | NodeFlags.SvgMember)
        {
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override IElement ParseSubtree(String html) => this.ParseHtmlSubtree(html);

        /// <inheritdoc />
        public override Node Clone(Document owner, Boolean deep)
        {
            var factory = Context.GetFactory<IElementFactory<Document, SvgElement>>();
            var node = factory.Create(owner, LocalName, Prefix);
            CloneElement(node, owner, deep);
            return node;
        }

        #endregion
    }
}

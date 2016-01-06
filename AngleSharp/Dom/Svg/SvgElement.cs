namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    internal class SvgElement : Element, ISvgElement
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
            var node = Factory.SvgElements.Create(Owner, LocalName, Prefix);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();

            var style = this.GetOwnAttribute(AttributeNames.Style);
            RegisterAttributeObserver(AttributeNames.Style, UpdateStyle);

            if (style != null)
            {
                UpdateStyle(style);
            }
        }

        #endregion
    }
}

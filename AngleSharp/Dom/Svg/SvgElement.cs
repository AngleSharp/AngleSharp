namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    class SvgElement : Element, ISvgElement
    {
        #region Fields

        ICssStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new SVG element.
        /// </summary>
        public SvgElement(Document owner, String name, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, Namespaces.SvgUri, flags | NodeFlags.SvgMember)
        {
            RegisterAttributeObserver(AttributeNames.Style, value =>
            {
                var bindable = _style as IBindable;

                if (String.IsNullOrEmpty(value))
                    Attributes.RemoveNamedItem(Attributes.Get(null, AttributeNames.Style));

                if (bindable != null)
                    bindable.Update(value);
            });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an object representing the declarations of an element's style
        /// attributes.
        /// </summary>
        public ICssStyleDeclaration Style
        {
            get { return _style ?? (_style = CreateStyle()); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.
        /// </param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = Factory.SvgElements.Create(Owner, LocalName, Prefix);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion
    }
}

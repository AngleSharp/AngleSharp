namespace AngleSharp.DOM.Svg
{
    using AngleSharp.DOM.Css;
    using System;

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    class SVGElement : Element, ISvgElement
    {
        #region Fields

        readonly CSSStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new SVG element.
        /// </summary>
        internal SVGElement(String name, NodeFlags flags = NodeFlags.None)
            : base(name, flags | NodeFlags.SvgMember)
        {
            NamespaceUri = Namespaces.SvgUri;
            _style = new CSSStyleDeclaration();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an object representing the declarations of an element's style attributes.
        /// </summary>
        public ICssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = SvgElementFactory.Create(NodeName, Owner);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion
    }
}

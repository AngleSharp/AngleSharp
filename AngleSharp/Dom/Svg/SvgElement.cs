namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    class SvgElement : Element, ISvgElement
    {
        #region Fields

        CssStyleDeclaration _style;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new SVG element.
        /// </summary>
        public SvgElement(Document owner, String name, NodeFlags flags = NodeFlags.None)
            : base(owner, name, null, Namespaces.SvgUri, flags | NodeFlags.SvgMember)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an object representing the declarations of an element's style attributes.
        /// </summary>
        public ICssStyleDeclaration Style
        {
            get { return _style ?? (_style = new CssStyleDeclaration()); }
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
            var node = Factory.SvgElements.Create(NodeName, Owner);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion
    }
}

using System;

namespace AngleSharp.DOM.Svg
{
    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    public sealed class SVGElement : Element
    {
        #region Constants

        /// <summary>
        /// The svg tag.
        /// </summary>
        internal const String RootTag = "svg";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new SVG element.
        /// </summary>
        internal SVGElement()
        {
            _ns = Namespaces.Svg;
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        internal static SVGElement Create(String tagName)
        {
            return new SVGElement { _name = tagName };
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if the current node is in the MathML namespace.
        /// </summary>
        internal protected override Boolean IsInSvg
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        protected internal override Boolean IsHtmlTIP
        {
            get { return IsSpecial; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return _name == "foreignObject" || _name == "desc" || _name == "title"; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var node = Create(_name);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion
    }
}

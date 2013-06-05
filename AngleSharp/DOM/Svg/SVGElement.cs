using System;

namespace AngleSharp.DOM.Svg
{
    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    public class SVGElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a new SVG element.
        /// </summary>
        public SVGElement()
        {
            NamespaceURI = Namespaces.Svg;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if the current node is in the MathML namespace.
        /// </summary>
        internal protected override bool IsInSvg
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the status if the node is an HTML text integration point.
        /// </summary>
        protected internal override bool IsHtmlTIP
        {
            get
            {
                var name = NodeName;
                return name == "foreignObject" || name == "desc" || name == "title";
            }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get
            {
                var name = NodeName;
                return name == "foreignObject" || name == "desc" || name == "title";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override Node CloneNode(bool deep = true)
        {
            var node = Factory(_name);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public static SVGElement Factory(string tagName)
        {
            return new SVGElement { _name = tagName };
        }

        #endregion
    }
}

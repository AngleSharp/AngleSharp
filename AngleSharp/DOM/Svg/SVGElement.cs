using System;

namespace AngleSharp.DOM.Svg
{
    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    [DOM("SVGElement")]
    public class SVGElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a new SVG element.
        /// </summary>
        internal SVGElement()
        {
            _ns = Namespaces.Svg;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if the current node is in the SVG namespace.
        /// </summary>
        internal protected override Boolean IsInSvg
        {
            get { return true; }
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
            var node = SVGFactory.Create(_name, _owner);
            CopyProperties(this, node, deep);
            return node;
        }

        #endregion
    }
}

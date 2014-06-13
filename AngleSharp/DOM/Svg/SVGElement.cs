namespace AngleSharp.DOM.Svg
{
    using System;

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    [DomName("SVGElement")]
    public class SVGElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a new SVG element.
        /// </summary>
        internal SVGElement()
        {
            NamespaceUri = Namespaces.Svg;
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
        [DomName("cloneNode")]
        public override INode Clone(Boolean deep = true)
        {
            var node = SVGFactory.Create(_name, _owner);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion
    }
}

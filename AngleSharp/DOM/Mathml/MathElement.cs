namespace AngleSharp.DOM.Mathml
{
    using System;

    /// <summary>
    /// Represents an element of the MathML DOM.
    /// </summary>
    class MathElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a new MathML element.
        /// </summary>
        internal MathElement()
        {
            _name = Tags.Math;
            NamespaceUri = Namespaces.MathML;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if this node is the MathML namespace.
        /// </summary>
        internal protected override Boolean IsInMathML
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
        public override INode Clone(Boolean deep = true)
        {
            var node = MathElementFactory.Create(_name, Owner);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion
    }
}

namespace AngleSharp.Dom.Mathml
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents an element of the MathML DOM.
    /// </summary>
    class MathElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a new MathML element.
        /// </summary>
        public MathElement(Document owner, String name, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, Namespaces.MathMlUri, flags | NodeFlags.MathMember)
        {
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
            var node = Factory.MathElements.Create(Owner, LocalName, Prefix);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
        }

        #endregion
    }
}

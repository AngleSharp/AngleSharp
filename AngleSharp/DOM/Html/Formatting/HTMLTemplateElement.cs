namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the template element.
    /// </summary>
    sealed class HTMLTemplateElement : HTMLElement, IScopeElement, ITableScopeElement, IHtmlTemplateElement
    {
        #region Fields

        DocumentFragment _content;

        #endregion

        #region ctor

        internal HTMLTemplateElement()
            : base(Tags.Template, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contents of this HTML template.
        /// </summary>
        public IDocumentFragment Content
        {
            get { return Container; }
        }

        #endregion

        #region Internal Properties

        internal DocumentFragment Container
        {
            get { return _content ?? (_content = new DocumentFragment { Owner = Owner }); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        public override INode AppendChild(INode child)
        {
            Content.AppendChild(child);
            return child;
        }

        /// <summary>
        /// Returns a duplicate of the template including the contents if deep is specified.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var clone = new HTMLTemplateElement();
            CopyProperties(this, clone, deep);
            CopyAttributes(this, clone);

            if (deep && _content != null)
            {
                clone._content = (DocumentFragment)_content.Clone(true);
                clone._content.Owner = Owner;
            }

            return clone;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml()
        {
            var sb = Pool.NewStringBuilder();

            sb.Append(Specification.LessThan).Append(NodeName);

            foreach (var attribute in Attributes)
                sb.Append(Specification.Space).Append(attribute.ToString());

            sb.Append(Specification.GreaterThan);

            foreach (var child in Content.ChildNodes)
                sb.Append(child.ToHtml());

            sb.Append(Specification.LessThan).Append(Specification.Solidus).Append(NodeName);

            return sb.Append(Specification.GreaterThan).ToPool();
        }

        #endregion
    }
}

using System;
using System.Text;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the template element.
    /// </summary>
    [DOM("HTMLTemplateElement")]
    public sealed class HTMLTemplateElement : HTMLElement, IScopeElement, ITableScopeElement
    {
        #region Members

        DocumentFragment _content;

        #endregion

        #region ctor

        internal HTMLTemplateElement()
        {
            _name = Tags.TEMPLATE;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contents of this HTML template.
        /// </summary>
        [DOM("content")]
        public DocumentFragment Content
        {
            get { return _content ?? (_content = new DocumentFragment { OwnerDocument = OwnerDocument }); }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a child to the collection of children.
        /// </summary>
        /// <param name="child">The child to add.</param>
        /// <returns>The added child.</returns>
        [DOM("appendChild")]
        public override Node AppendChild(Node child)
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
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var clone = new HTMLTemplateElement();
            CopyProperties(this, clone, deep);

            if (deep && _content != null)
            {
                clone._content = (DocumentFragment)_content.CloneNode(true);
                clone._content.OwnerDocument = OwnerDocument;
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
            var sb = new StringBuilder();

            sb.Append('<').Append(_name);
            sb.Append(_attributes.ToHtml());
            sb.Append(">");

            foreach (var child in Content.ChildNodes)
                sb.Append(child.ToHtml());

            sb.Append("</").Append(_name).Append('>');
            return sb.ToString();
        }

        #endregion
    }
}

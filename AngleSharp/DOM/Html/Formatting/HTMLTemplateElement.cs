using System;

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
    }
}

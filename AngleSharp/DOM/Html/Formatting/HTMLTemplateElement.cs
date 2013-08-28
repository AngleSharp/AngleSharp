using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the template element.
    /// </summary>
    [DOM("HTMLTemplateElement")]
    public sealed class HTMLTemplateElement : HTMLElement, IScopeElement
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
            get { return _content ?? (_content = new DocumentFragment()); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}

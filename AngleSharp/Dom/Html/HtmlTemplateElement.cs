namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the template element.
    /// </summary>
    sealed class HtmlTemplateElement : HtmlElement, IHtmlTemplateElement
    {
        #region Fields

        readonly DocumentFragment _content;

        #endregion

        #region ctor

        public HtmlTemplateElement(Document owner, String prefix = null)
            : base(owner, Tags.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
            _content = new DocumentFragment(owner);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contents of this HTML template.
        /// </summary>
        public IDocumentFragment Content
        {
            get { return _content; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Moves the children of the template element to the content.
        /// </summary>
        public void PopulateFragment()
        {
            while (HasChildNodes)
            {
                var node = ChildNodes[0];
                RemoveNode(0, node);
                _content.AddNode(node);
            }
        }

        /// <summary>
        /// Returns a duplicate of the template including the contents if deep is specified.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var clone = new HtmlTemplateElement(Owner);
            CopyProperties(this, clone, deep);
            CopyAttributes(this, clone);

            for (int i = 0; i < _content.ChildNodes.Length; i++)
            {
                var node = _content.ChildNodes[i].Clone(deep) as Node;

                if (node != null)
                    clone._content.AddNode(node);
            }

            return clone;
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            var open = formatter.OpenTag(this, false);
            var children = _content.ChildNodes.ToHtml(formatter);
            var close = formatter.CloseTag(this, false);
            return String.Concat(open, children, close);
        }

        #endregion

        #region Helpers

        internal override void NodeIsAdopted(Document oldDocument)
        {
            _content.Owner = oldDocument;
        }

        #endregion
    }
}

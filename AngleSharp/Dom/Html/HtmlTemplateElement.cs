namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

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
            : base(owner, TagNames.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
            _content = new DocumentFragment(owner);
        }

        #endregion

        #region Properties

        public IDocumentFragment Content
        {
            get { return _content; }
        }

        #endregion

        #region Methods

        public void PopulateFragment()
        {
            while (HasChildNodes)
            {
                var node = ChildNodes[0];
                RemoveNode(0, node);
                _content.AddNode(node);
            }
        }

        public override INode Clone(Boolean deep = true)
        {
            var clone = new HtmlTemplateElement(Owner);
            CopyProperties(this, clone, deep);
            CopyAttributes(this, clone);

            for (var i = 0; i < _content.ChildNodes.Length; i++)
            {
                var node = _content.ChildNodes[i].Clone(deep) as Node;

                if (node != null)
                {
                    clone._content.AddNode(node);
                }
            }

            return clone;
        }

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

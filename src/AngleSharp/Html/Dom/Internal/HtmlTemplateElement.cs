namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.IO;

    /// <summary>
    /// Represents the template element.
    /// </summary>
    sealed class HtmlTemplateElement : HtmlElement, IHtmlTemplateElement
    {
        #region Fields

        private readonly DocumentFragment _content;

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

        public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            writer.Write(formatter.OpenTag(this, false));
            _content.ChildNodes.ToHtml(writer, formatter);
            writer.Write(formatter.CloseTag(this, false));
        }

        #endregion

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var template = new HtmlTemplateElement(owner);
            CloneElement(template, owner, deep);
            var clonedContent = template._content;

            foreach (var child in _content.ChildNodes)
            {
                var node = child as Node;

                if (node != null)
                {
                    var clone = node.Clone(owner, deep);
                    clonedContent.AddNode(clone);
                }
            }

            return template;
        }

        protected override void NodeIsAdopted(Document oldDocument)
        {
            _content.Owner = oldDocument;
        }

        #endregion
    }
}

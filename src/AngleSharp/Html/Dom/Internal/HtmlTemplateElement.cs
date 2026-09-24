namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;
    using Construction;

    /// <summary>
    /// Represents the template element.
    /// </summary>
    sealed class HtmlTemplateElement : HtmlElement, IHtmlTemplateElement, IConstructableTemplateElement
    {
        #region Fields

        private readonly DocumentFragment _content;
        private Int32 _contentReplacementDepth;

        #endregion

        #region ctor

        public HtmlTemplateElement(Document owner, String? prefix = null)
            : base(owner, TagNames.Template, prefix, NodeFlags.Special | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
            _content = new DocumentFragment(owner);
        }

        #endregion

        #region Properties

        public IDocumentFragment Content => _content;

        internal Boolean IsStagingContent => _contentReplacementDepth != 0;

        #endregion

        #region Methods

        internal override void ReplaceAll(Node? node, Boolean suppressObservers)
        {
            _contentReplacementDepth++;

            try
            {
                base.ReplaceAll(node, suppressObservers);
            }
            finally
            {
                _contentReplacementDepth--;
            }
        }

        public override Node Clone(Document owner, Boolean deep)
        {
            var template = new HtmlTemplateElement(owner);
            CloneElement(template, owner, deep);
            var clonedContent = template._content;

            foreach (var child in _content.ChildNodes)
            {
                if (child is Node node)
                {
                    var clone = node.Clone(owner, deep);
                    clonedContent.AddNode(clone);
                }
            }

            return template;
        }

        public void PopulateFragment()
        {
            while (HasChildNodes)
            {
                var node = ChildNodes[0];
                RemoveNode(0, node);
                _content.AddNode(node);
            }

            // InnerHtml stages children through DOM insertion before this move;
            // construction moves them directly. Neither leaves an active base.
            Owner.RefreshBaseUrl();
        }

        #endregion

        #region Helpers

        protected override void ReplacedAll() => PopulateFragment();

        protected override void NodeIsAdopted(Document oldDocument) => _content.Owner = oldDocument;

        #endregion
    }
}

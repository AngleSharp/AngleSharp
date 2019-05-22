namespace AngleSharp.Dom
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a shadow root.
    /// </summary>
    sealed class ShadowRoot : Node, IShadowRoot
    {
        #region Fields

        private readonly Element _host;
        private readonly IStyleSheetList _styleSheets;
        private readonly ShadowRootMode _mode;

        HtmlCollection<IElement> _elements;

        #endregion

        #region ctor

        internal ShadowRoot(Element host, ShadowRootMode mode)
            : base(host.Owner, "#shadow-root", NodeType.DocumentFragment)
        {
            _host = host;
            _styleSheets = this.CreateStyleSheets();
            _mode = mode;
        }

        #endregion

        #region Properties

        public IElement ActiveElement => this.GetDescendants().OfType<Element>().Where(m => m.IsFocused).FirstOrDefault();

        public IElement Host => _host;

        public String InnerHtml
        {
            get => ChildNodes.ToHtml(HtmlMarkupFormatter.Instance);
            set => ReplaceAll(new DocumentFragment(_host, value), false);
        }

        public IStyleSheetList StyleSheets => _styleSheets;

        public Int32 ChildElementCount => ChildNodes.OfType<Element>().Count();

        public IHtmlCollection<IElement> Children => _elements ?? (_elements = new HtmlCollection<IElement>(this, deep: false));

        public IElement FirstElementChild
        {
            get
            {
                var children = ChildNodes;
                var n = children.Length;

                for (var i = 0; i < n; i++)
                {
                    if (children[i] is IElement child)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        public IElement LastElementChild
        {
            get
            {
                var children = ChildNodes;

                for (var i = children.Length - 1; i >= 0; i--)
                {
                    if (children[i] is IElement child)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        public override String TextContent
        {
            get
            {
                var sb = StringBuilderPool.Obtain();

                foreach (var child in this.GetDescendants().OfType<IText>())
                {
                    sb.Append(child.Data);
                }

                return sb.ToPool();
            }
            set
            {
                var node = !String.IsNullOrEmpty(value) ? new TextNode(Owner, value) : null;
                ReplaceAll(node, false);
            }
        }

        #endregion

        #region Methods

        public void Prepend(params INode[] nodes) => this.PrependNodes(nodes);

        public void Append(params INode[] nodes) => this.AppendNodes(nodes);

        public IElement QuerySelector(String selectors) => ChildNodes.QuerySelector(selectors, _host);

        public IHtmlCollection<IElement> QuerySelectorAll(String selectors) => ChildNodes.QuerySelectorAll(selectors, _host);

        public IHtmlCollection<IElement> GetElementsByClassName(String classNames) => ChildNodes.GetElementsByClassName(classNames);

        public IHtmlCollection<IElement> GetElementsByTagName(String tagName) => ChildNodes.GetElementsByTagName(tagName);

        public IHtmlCollection<IElement> GetElementsByTagNameNS(String namespaceURI, String tagName) => ChildNodes.GetElementsByTagName(namespaceURI, tagName);

        public IElement GetElementById(String elementId) => ChildNodes.GetElementById(elementId);

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = new ShadowRoot(_host, _mode);
            CloneNode(node, owner, deep);
            return node;
        }

        #endregion
    }
}

namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a shadow root.
    /// </summary>
    sealed class ShadowRoot : Node, IShadowRoot
    {
        #region Fields

        readonly Element _host;
        readonly IStyleSheetList _styleSheets;
        readonly ShadowRootMode _mode;

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

        public IElement ActiveElement
        {
            get { return this.GetDescendants().OfType<Element>().Where(m => m.IsFocused).FirstOrDefault(); }
        }

        public IElement Host
        {
            get { return _host; }
        }

        public String InnerHtml
        {
            get { return ChildNodes.ToHtml(HtmlMarkupFormatter.Instance); }
            set { ReplaceAll(new DocumentFragment(_host, value), false); }
        }

        public IStyleSheetList StyleSheets
        {
            get { return _styleSheets; }
        }

        public Int32 ChildElementCount
        {
            get { return ChildNodes.OfType<Element>().Count(); }
        }

        public IHtmlCollection<IElement> Children
        {
            get { return _elements ?? (_elements = new HtmlCollection<IElement>(this, deep: false)); }
        }

        public IElement FirstElementChild
        {
            get
            {
                var children = ChildNodes;
                var n = children.Length;

                for (var i = 0; i < n; i++)
                {
                    var child = children[i] as IElement;

                    if (child != null)
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
                    var child = children[i] as IElement;

                    if (child != null)
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
                var sb = Pool.NewStringBuilder();

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

        public override INode Clone(Boolean deep = true)
        {
            var node = new ShadowRoot(_host, _mode);
            CloneNode(node, deep);
            return node;
        }

        public void Prepend(params INode[] nodes)
        {
            this.PrependNodes(nodes);
        }

        public void Append(params INode[] nodes)
        {
            this.AppendNodes(nodes);
        }

        public IElement QuerySelector(String selectors)
        {
            return ChildNodes.QuerySelector(selectors);
        }

        public IHtmlCollection<IElement> QuerySelectorAll(String selectors)
        {
            return ChildNodes.QuerySelectorAll(selectors);
        }

        public IHtmlCollection<IElement> GetElementsByClassName(String classNames)
        {
            return ChildNodes.GetElementsByClassName(classNames);
        }

        public IHtmlCollection<IElement> GetElementsByTagName(String tagName)
        {
            return ChildNodes.GetElementsByTagName(tagName);
        }

        public IHtmlCollection<IElement> GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return ChildNodes.GetElementsByTagName(namespaceURI, tagName);
        }

        public IElement GetElementById(String elementId)
        {
            return ChildNodes.GetElementById(elementId);
        }

        public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            ChildNodes.ToHtml(writer, formatter);
        }

        #endregion
    }
}

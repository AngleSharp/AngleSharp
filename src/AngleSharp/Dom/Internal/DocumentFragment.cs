namespace AngleSharp.Dom
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Text;
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a document fragment.
    /// </summary>
    sealed class DocumentFragment : Node, IDocumentFragment
    {
        #region Fields

        private HtmlCollection<IElement> _elements;

        #endregion

        #region ctor

        internal DocumentFragment(Document owner)
            : base(owner, "#document-fragment", NodeType.DocumentFragment)
        {
        }

        internal DocumentFragment(Element contextElement, String html)
            : this(contextElement.Owner)
        {
            var source = new TextSource(html);
            var context = Owner.Context;
            var document = new HtmlDocument(context, source);
            var parser = new HtmlDomBuilder(document);
            var options = new HtmlParserOptions
            {
                IsEmbedded = false,
                IsStrictMode = false,
                IsScripting = context.IsScripting()
            };
            var root = parser.ParseFragment(options, contextElement).DocumentElement;

            while (root.HasChildNodes)
            {
                var child = root.FirstChild;
                root.RemoveChild(child);
                if (child is Node)
                {
                    Owner.AdoptNode(child);
                    InsertBefore((Node)child, null, false);
                }
            }
        }

        #endregion

        #region Properties

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

                for (int i = 0; i < n; i++)
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

                for (int i = children.Length - 1; i >= 0; i--)
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
            return ChildNodes.QuerySelector(selectors, null);
        }

        public IHtmlCollection<IElement> QuerySelectorAll(String selectors)
        {
            return ChildNodes.QuerySelectorAll(selectors, null);
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

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var node = new DocumentFragment(owner);
            CloneNode(node, owner, deep);
            return node;
        }

        #endregion
    }
}

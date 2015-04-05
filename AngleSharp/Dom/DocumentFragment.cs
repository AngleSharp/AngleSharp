namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Html;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a document fragment.
    /// </summary>
    sealed class DocumentFragment : Node, IDocumentFragment
    {
        #region ctor

        /// <summary>
        /// Creates a new document fragment.
        /// </summary>
        internal DocumentFragment(Document owner)
            : base(owner, "#document-fragment", NodeType.DocumentFragment)
        {
        }

        /// <summary>
        /// Creates a new document fragment with the given nodelist as
        /// children.
        /// </summary>
        /// <param name="context">The context for the fragment mode.</param>
        /// <param name="html">The HTML source code to use.</param>
        internal DocumentFragment(Element context, String html)
            : this(context.Owner)
        {
            var parser = new HtmlParser(html, Owner.Options);
            parser.IsScripting = false;
            var root = parser.ParseFragment(context).DocumentElement;

            while (root.HasChildNodes)
            {
                var child = root.FirstChild;
                root.RemoveChild(child);
                this.PreInsert(child, null);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public Int32 ChildElementCount
        {
            get { return ChildNodes.OfType<Element>().Count(); }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        public IHtmlCollection<IElement> Children
        {
            get { return new HtmlElementCollection(ChildNodes.OfType<Element>()); }
        }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
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
                        return child;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the last child element of this element.
        /// </summary>
        public IElement LastElementChild
        {
            get
            {
                var children = ChildNodes;

                for (int i = children.Length - 1; i >= 0; i--)
                {
                    var child = children[i] as IElement;

                    if (child != null)
                        return child;
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
                    sb.Append(child.Data);

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

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.
        /// </param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new DocumentFragment(Owner);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Prepends nodes to the current document fragment.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        public void Prepend(params INode[] nodes)
        {
            this.PrependNodes(nodes);
        }

        /// <summary>
        /// Appends nodes to current document fragment.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        public void Append(params INode[] nodes)
        {
            this.AppendNodes(nodes);
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first
        /// pre-order traversal of the document's nodes) that matches the
        /// specified group of selectors.
        /// </summary>
        /// <param name="selectors">
        /// A string containing one or more CSS selectors separated by commas.
        /// </param>
        /// <returns>An element object.</returns>
        public IElement QuerySelector(String selectors)
        {
            return ChildNodes.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using
        /// depth-first pre-order traversal of the document's nodes) that match
        /// the specified group of selectors.
        /// </summary>
        /// <param name="selectors">
        /// A string containing one or more CSS selectors separated by commas.
        /// </param>
        /// <returns>An element object.</returns>
        public IHtmlCollection<IElement> QuerySelectorAll(String selectors)
        {
            return ChildNodes.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">
        /// A string representing the list of class names to match; class names
        /// are separated by whitespace.
        /// </param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection<IElement> GetElementsByClassName(String classNames)
        {
            return ChildNodes.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The
        /// complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">
        /// A string representing the name of the elements. The special string
        /// "*" represents all elements.
        /// </param>
        /// <returns>
        /// A NodeList of found elements in the order they appear in the tree.
        /// </returns>
        public IHtmlCollection<IElement> GetElementsByTagName(String tagName)
        {
            return ChildNodes.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the
        /// given namespace. The complete document is searched, including the
        /// root node.
        /// </summary>
        /// <param name="namespaceURI">
        /// The namespace URI of elements to look for.
        /// </param>
        /// <param name="tagName">
        /// Either the local name of elements to look for or the special value
        /// "*", which matches all elements.
        /// </param>
        /// <returns>
        /// A NodeList of found elements in the order they appear in the tree.
        /// </returns>
        public IHtmlCollection<IElement> GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return ChildNodes.GetElementsByTagName(namespaceURI, tagName);
        }

        /// <summary>
        /// Tries to get the an element in the DOM tree given its id.
        /// </summary>
        /// <param name="elementId">The id of the element to get.</param>
        /// <returns>The element with the corresponding ID or null.</returns>
        public IElement GetElementById(String elementId)
        {
            return ChildNodes.GetElementById(elementId);
        }

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            return ChildNodes.ToHtml(formatter);
        }

        #endregion
    }
}

using System;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Mathml;
using AngleSharp.DOM.Svg;
using AngleSharp.DOM.Xml;
using AngleSharp.DOM.Collections;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a document node.
    /// </summary>
    public class Document : Node, IDocument, IDocumentStyle
    {
        #region Members

        QuirksMode quirksMode;
        Readiness ready;
        StyleSheetList styleSheets;
        DOMImplementation implementation;

        string encoding;
        string originalEncoding;

        protected string referrer;
        protected string location;
        protected Element documentElement;
        protected DocumentType docType;

        #endregion

        #region Events

        public event EventHandler ReadyStateChange;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        public Document()
        {
            _owner = this;
            NodeType = NodeType.Document;
            Async = true;
            referrer = string.Empty;
            ready = Readiness.Complete;
            _name = "#document";
            implementation = new DOMImplementation();
            styleSheets = new StyleSheetList();
            quirksMode = QuirksMode.Off;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the DOMImplementation object that handles this document.
        /// </summary>
        public DOMImplementation Implementation
        {
            get { return implementation; }
        }

        /// <summary>
        /// Gets a string containing the date and time on which the current document was last modified.
        /// </summary>
        public DateTime LastModified
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public DocumentType DocType
        {
            get { return docType; }
        }

        /// <summary>
        /// Gets or sets the ready state of the document.
        /// </summary>
        public Readiness ReadyState
        {
            get
            {
                return ready;
            }
            set
            {
                ready = value;

                if (ReadyStateChange != null)
                    ReadyStateChange(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets a list of stylesheet objects for stylesheets explicitly linked into or embedded in a document.
        /// </summary>
        public StyleSheetList StyleSheets
        {
            get { return styleSheets; }
        }

        /// <summary>
        /// Gets a live list of all of the currently-available style sheet sets.
        /// </summary>
        public StringList StyleSheetSets
        {
            get
            {
                var list = new StringList();

                for (int i = 0; i < styleSheets.Length; i++)
                    list.Add(styleSheets[i].Title);

                return list;
            }
        }

        /// <summary>
        /// Gets the URI of the page that linked to this page.
        /// </summary>
        public string Referrer
        {
            get { return referrer; }
            internal protected set { referrer = value; }
        }

        /// <summary>
        /// Gets or sets the URI of the current document.
        /// </summary>
        public Location Location
        {
            get { return new Location(location); }
            set { location = value.ToString(); }
        }

        /// <summary>
        /// Gets the URI of the current document.
        /// </summary>
        public string DocumentURI
        {
            get { return location; }
        }

        /// <summary>
        /// Gets the window object associated with the document or null if none available.
        /// </summary>
        public object DefaultView { get; internal set; }//TODO

        /// <summary>
        /// Gets or sets the character encoding of the current document.
        /// </summary>
        public string CharacterSet
        {
            get { return encoding ?? originalEncoding; }
            set { encoding = value; }
        }

        /// <summary>
        /// Gets the encoding that was used when the document was parsed.
        /// </summary>
        public string InputEncoding
        {
            get { return originalEncoding; }
            internal set 
            { 
                originalEncoding = value;

                if (encoding != null)
                    encoding = value; 
            }
        }

        /// <summary>
        /// Gets the root element of the document.
        /// </summary>
        public Element DocumentElement
        {
            get { return documentElement; }
        }

        /// <summary>
        /// Gets the currently focused element, that is, the element that will get keystroke events if the user types any.
        /// </summary>
        public Element ActiveElement { get; protected set; }

        /// <summary>
        /// Gets or sets to indicate whether loading the document should be an asynchronous or synchronous.
        /// </summary>
        public bool Async { get; set; }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets or sets the status of the quirks mode of the document.
        /// </summary>
        internal QuirksMode QuirksMode
        {
            get { return quirksMode; }
            set { quirksMode = value; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Dereferences a child element (use when removing any child).
        /// </summary>
        /// <param name="node">The node to be removed.</param>
        internal virtual void DereferenceNode(Node node)
        {
            if (node == documentElement)
                documentElement = FindChild<Element>(this);
            else if (docType == node)
                docType = FindChild<DocumentType>(this);
        }

        /// <summary>
        /// References a child element (use when adding any child).
        /// </summary>
        /// <param name="node">The node to be added.</param>
        internal virtual void ReferenceNode(Node node)
        {
            if (documentElement == null && node is Element)
                documentElement = (Element)node;
            else if (docType == null && node is DocumentType)
                docType = (DocumentType)node;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an event of the type specified.
        /// </summary>
        /// <param name="type">A string that represents the type of event to be created.</param>
        /// <returns>The created Event object.</returns>
        public Event CreateEvent(string type)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a new Range object.
        /// </summary>
        /// <returns>The created range object.</returns>
        public Range CreateRange()
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prepends nodes to the current document.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        /// <returns>The current document.</returns>
        public Document Prepend(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                InsertChild(0, node);
            }

            return this;
        }

        /// <summary>
        /// Appends nodes to current document.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        /// <returns>The current document.</returns>
        public Document Append(params Node[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                AppendChild(node);
            }

            return this;
        }

        /// <summary>
        /// Creates a copy of a node from an external document that can be inserted into the current document.
        /// </summary>
        /// <param name="externalNode">The node from another document to be imported.</param>
        /// <param name="deep">Optional argument, indicating whether the descendants of the imported
        /// node need to be imported.</param>
        /// <returns>The new node that is imported into the document. The new node's parentNode is null,
        /// since it has not yet been inserted into the document tree.</returns>
        public Node ImportNode(Node externalNode, bool deep = true)
        {
            var clone = externalNode.CloneNode(deep);
            externalNode.OwnerDocument = this;
            return externalNode;
        }

        /// <summary>
        /// Creates a new attribute node, and returns it.
        /// </summary>
        /// <param name="name">A string containing the name of the attribute.</param>
        /// <returns>The attribute node.</returns>
        public Attr CreateAttribute(string name)
        {
            return new Attr(name);
        }

        /// <summary>
        /// Creates a new attribute node with a namespace, and returns it.
        /// </summary>
        /// <param name="namespaceURI">Specifies the namespace URI to associate with the attribute.</param>
        /// <param name="name">A string containing the name of the attribute.</param>
        /// <returns>The attribute node.</returns>
        public Attr CreateAttributeNS(string namespaceURI, string name)
        {
            return new Attr(name, string.Empty, namespaceURI);
        }

        /// <summary>
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element object.</returns>
        public virtual Element CreateElement(string tagName)
        {
            return new Element { NodeName = tagName };
        }

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceURI">Specifies the namespace URI to associate with the element.</param>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element.</returns>
        public Element CreateElementNS(string namespaceURI, string tagName)
        {
            if (namespaceURI == Namespaces.Html)
                return HTMLElement.Factory(tagName);
            else if (namespaceURI == Namespaces.Svg)
                return SVGElement.Factory(tagName);
            else if (namespaceURI == Namespaces.MathML)
                return MathMLElement.Factory(tagName);
            else if (namespaceURI == Namespaces.Xml)
                return XMLElement.Factory(tagName);

            return new Element { NamespaceURI = namespaceURI, NodeName = tagName };
        }

        /// <summary>
        /// Creates a new CDATA section node, and returns it.
        /// </summary>
        /// <param name="data">A string containing the data to be added to the CDATA Section.</param>
        /// <returns></returns>
        public virtual CDATASection CreateCDATASection(string data)
        {
            return new CDATASection { Data = data };
        }

        /// <summary>
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">A string containing the data to be added to the Comment.</param>
        /// <returns></returns>
        public Comment CreateComment(string data)
        {
            if (data.Contains("--"))
                throw new DOMException(ErrorCode.InvalidCharacter);

            return new Comment(data);
        }

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>A new document fragment.</returns>
        public DocumentFragment CreateDocumentFragment()
        {
            return new DocumentFragment();
        }

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and data strings.
        /// </summary>
        /// <param name="target">The target part of the processing instruction.</param>
        /// <param name="data">The data for the node.</param>
        /// <returns>A new processing instruction.</returns>
        public ProcessingInstruction CreateProcessingInstruction(string target, string data)
        {
            return new ProcessingInstruction { Target = target, Data = data };
        }

        /// <summary>
        /// Creates an EntityReference object. In addition, if the referenced entity is known,
        /// the child list of the EntityReference node is made the same as that of the corresponding
        /// Entity node.
        /// </summary>
        /// <param name="name">The name of the entity to reference.</param>
        /// <returns>The new EntityReference object.</returns>
        public EntityReference CreateEntityReference(string name)
        {
            return new EntityReference(name);
        }

        /// <summary>
        /// Creates a new Text node.
        /// </summary>
        /// <param name="data">A string containing the data to be put in the text node.</param>
        /// <returns>The created Text node.</returns>
        public TextNode CreateTextNode(string data)
        {
            return new TextNode(data);
        }

        /// <summary>
        /// Returns the Element whose ID is given by elementId. If no such element exists, returns null.
        /// The behavior is not defined if more than one element have this ID.
        /// </summary>
        /// <param name="elementId">A case-sensitive string representing the unique ID of the element being sought.</param>
        /// <returns>The matching element.</returns>
        public Element GetElementById(string elementId)
        {
            return GetElementById(_children, elementId);
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        public Element QuerySelector(string selectors)
        {
            return _children.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A list of nodes.</returns>
        public HTMLCollection QuerySelectorAll(string selectors)
        {
            return _children.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of elements.</returns>
        public HTMLCollection GetElementsByClassName(string classNames)
        {
            return _children.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        public HTMLCollection GetElementsByTagName(string tagName)
        {
            return _children.GetElementsByTagName(tagName);
        }

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="namespaceURI">The namespace URI of elements to look for.</param>
        /// <param name="tagName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        public HTMLCollection GetElementsByTagNameNS(string namespaceURI, string tagName)
        {
            return _children.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override Node CloneNode(bool deep = true)
        {
            var node = new Document();
            CopyProperties(this, node, deep);
            CopyDocumentProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Takes a prefix and returns the namespaceURI associated with it on the given node if found (and null if not).
        /// Supplying null for the prefix will return the default namespace.
        /// </summary>
        /// <param name="prefix">The prefix to look for.</param>
        /// <returns>The namespace URI.</returns>
        public override string LookupNamespaceURI(string prefix)
        {
            var root = DocumentElement;

            if (root != null)
                return root.LookupNamespaceURI(prefix);

            return null;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        public override string LookupPrefix(string namespaceURI)
        {
            var root = DocumentElement;

            if(root != null)
                return root.LookupPrefix(namespaceURI);

            return null;
        }

        /// <summary>
        /// Accepts a namespace URI as an argument and returns true if the namespace is the default namespace on the given node or false if not.
        /// </summary>
        /// <param name="namespaceURI">A string representing the namespace against which the element will be checked.</param>
        /// <returns>True if the given namespaceURI is the default namespace.</returns>
        public override bool IsDefaultNamespace(string namespaceURI)
        {
            var root = DocumentElement;

            if (root != null)
                return root.IsDefaultNamespace(namespaceURI);

            return false;
        }

        /// <summary>
        /// Acts as if the document was going through a save and load cycle, putting the document in a "normal"
        /// form. Normalizes all text nodes and fixes namespaces.
        /// </summary>
        /// <returns>The current document.</returns>
        public override Node Normalize()
        {
            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].Normalize();

                if (_children[i] is Element)
                    ((Element)_children[i]).NormalizeNamespaces();
            }

            return this;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the document.
        /// </summary>
        /// <returns>A string containing the HTML code.</returns>
        public override string ToHtml()
        {
            return ChildNodes.ToHtml();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Tries to find a direct child of a certain type.
        /// </summary>
        /// <param name="parent">The parent that contains the elements.</param>
        /// <typeparam name="T">The node type to find.</typeparam>
        /// <returns>The instance or null.</returns>
        protected static T FindChild<T>(Node parent) where T : Node
        {
            if (parent == null)
                return null;

            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                if (parent.ChildNodes[i] is T)
                    return (T)parent.ChildNodes[i];
            }

            return null;
        }

        /// <summary>
        /// Gets an element by its ID.
        /// </summary>
        /// <param name="children">The nodelist to investigate.</param>
        /// <param name="id">The id to find.</param>
        /// <returns>The element or NULL.</returns>
        static protected Element GetElementById(NodeList children, string id)
        {
            for (int i = 0; i < children.Length; i++)
            {
                var element = children[i] as Element;

                if (element != null)
                {
                    if (element.GetAttribute("id") == id)
                        return element;

                    element = GetElementById(element.ChildNodes, id);

                    if (element != null)
                        return element;
                }
            }

            return null;
        }

        /// <summary>
        /// Copies all (Document) properties of the source to the target.
        /// </summary>
        /// <param name="source">The source document.</param>
        /// <param name="target">The target document.</param>
        /// <param name="deep">Is a deep-copy required?</param>
        static protected void CopyDocumentProperties(Document source, Document target, bool deep)
        {
            target.ready = source.ready;
            target.referrer = source.referrer;
            target.location = source.location;
            target.implementation = source.implementation;
            target.quirksMode = source.quirksMode;
        }

        #endregion
    }
}

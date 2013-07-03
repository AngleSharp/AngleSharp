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
    [DOM("Document")]
    public class Document : Node, IDocument, IDocumentStyle
    {
        #region Members

        QuirksMode _quirksMode;
        Readiness _ready;
        DOMImplementation _implementation;

        String _encoding;
        String _originalEncoding;
        
        /// <summary>
        /// The content type of the MIME type from the header.
        /// </summary>
        protected String _contentType;
        /// <summary>
        /// The list of contained stylesheets.
        /// </summary>
        protected StyleSheetList _styleSheets;
        /// <summary>
        /// The original referrer to this document.
        /// </summary>
        protected String _referrer;
        /// <summary>
        /// The location of the document.
        /// </summary>
        protected String _location;
        /// <summary>
        /// The root element.
        /// </summary>
        protected Element _documentElement;
        /// <summary>
        /// The doctype element.
        /// </summary>
        protected DocumentType _docType;

        #endregion

        #region Events

        /// <summary>
        /// This event is fired when the ready state of the document changes.
        /// </summary>
        [DOM("onreadystatechange")]
        public event EventHandler OnReadyStateChange;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        internal Document()
        {
            _owner = this;
            _type = NodeType.Document;
            Async = true;
            _referrer = string.Empty;
            _ready = Readiness.Complete;
            _name = "#document";
            _implementation = new DOMImplementation();
            _styleSheets = new StyleSheetList();
            _quirksMode = QuirksMode.Off;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an indicator if loading the document should be asynchronous or synchronous.
        /// </summary>
        [DOM("async")]
        public Boolean Async
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the DOMImplementation object that handles this document.
        /// </summary>
        [DOM("implementation")]
        public DOMImplementation Implementation
        {
            get { return _implementation; }
        }

        /// <summary>
        /// Gets a string containing the date and time on which the current document was last modified.
        /// </summary>
        [DOM("lastModified")]
        public DateTime LastModified
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        [DOM("doctype")]
        public DocumentType Doctype
        {
            get { return _docType; }
        }

        /// <summary>
        /// Gets the Content-Type from the MIME Header of the current document.
        /// </summary>
        [DOM("contentType")]
        public String ContentType
        {
            get { return _contentType; }
        }

        /// <summary>
        /// Gets or sets the ready state of the document.
        /// </summary>
        [DOM("readyState")]
        public Readiness ReadyState
        {
            get { return _ready; }
            set
            {
                _ready = value;

                if (OnReadyStateChange != null)
                    OnReadyStateChange(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets a list of stylesheet objects for stylesheets explicitly linked into or embedded in a document.
        /// </summary>
        [DOM("styleSheets")]
        public StyleSheetList StyleSheets
        {
            get { return _styleSheets; }
        }

        /// <summary>
        /// Gets a live list of all of the currently-available style sheet sets.
        /// </summary>
        [DOM("styleSheetSets")]
        public DOMStringList StyleSheetSets
        {
            get
            {
                var list = new DOMStringList();

                for (int i = 0; i < _styleSheets.Length; i++)
                    list.Add(_styleSheets[i].Title);

                return list;
            }
        }

        /// <summary>
        /// Gets the URI of the page that linked to this page.
        /// </summary>
        [DOM("referrer")]
        public String Referrer
        {
            get { return _referrer; }
            internal protected set { _referrer = value; }
        }

        /// <summary>
        /// Gets or sets the URI of the current document.
        /// </summary>
        [DOM("location")]
        public Location Location
        {
            get { return new Location(_location); }
            set { _location = value.ToString(); }
        }

        /// <summary>
        /// Gets the URI of the current document.
        /// </summary>
        [DOM("documentURI")]
        public String DocumentURI
        {
            get { return _location; }
        }

        /// <summary>
        /// Gets the window object associated with the document or null if none available.
        /// </summary>
        [DOM("defaultView")]
        public IWindow DefaultView 
        {
            get; //TODO
            internal set; 
        }

        /// <summary>
        /// Gets the parent window object if any.
        /// </summary>
        [DOM("parentWindow")]
        public IWindow ParentWindow
        {
            get { return DefaultView; }
        }

        /// <summary>
        /// Gets or sets the character encoding of the current document.
        /// </summary>
        [DOM("characterSet")]
        public String CharacterSet
        {
            get { return _encoding ?? _originalEncoding; }
            set { _encoding = value; }
        }

        /// <summary>
        /// Gets the encoding that was used when the document was parsed.
        /// </summary>
        [DOM("inputEncoding")]
        public String InputEncoding
        {
            get { return _originalEncoding; }
            internal set 
            { 
                _originalEncoding = value;

                if (_encoding != null)
                    _encoding = value; 
            }
        }

        /// <summary>
        /// Gets the root element of the document.
        /// </summary>
        [DOM("documentElement")]
        public Element DocumentElement
        {
            get { return _documentElement; }
        }

        /// <summary>
        /// Gets the currently focused element, that is, the element that will get keystroke events if the user types any.
        /// </summary>
        [DOM("activeElement")]
        public Element ActiveElement 
        {
            get;
            protected set; 
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets or sets the status of the quirks mode of the document.
        /// </summary>
        internal QuirksMode QuirksMode
        {
            get { return _quirksMode; }
            set { _quirksMode = value; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Dereferences a child element (use when removing any child).
        /// </summary>
        /// <param name="node">The node to be removed.</param>
        internal virtual void DereferenceNode(Node node)
        {
            if (node == _documentElement)
                _documentElement = FindChild<Element>(this);
            else if (_docType == node)
                _docType = FindChild<DocumentType>(this);
            else if (node is HTMLStyleElement)
                _styleSheets.Remove(((HTMLStyleElement)node).Sheet);
            else if (node is HTMLLinkElement)
            {
                var link = (HTMLLinkElement)node;

                switch (link.Rel)
                {
                    case "stylesheet":
                        _styleSheets.Remove(link.Sheet);
                        break;
                }
            }
        }

        /// <summary>
        /// References a child element (use when adding any child).
        /// </summary>
        /// <param name="node">The node to be added.</param>
        internal virtual void ReferenceNode(Node node)
        {
            if (_documentElement == null && node is Element)
                _documentElement = (Element)node;
            else if (_docType == null && node is DocumentType)
                _docType = (DocumentType)node;
            else if (node is HTMLStyleElement)
                _styleSheets.Add(((HTMLStyleElement)node).Sheet);
            else if (node is HTMLLinkElement)
            {
                var link = (HTMLLinkElement)node;

                switch (link.Rel)
                {
                    // ext. resources
                    case "prefetch":
                    case "icon":
                    case "pingback":
                        break;

                    case "stylesheet":
                        _styleSheets.Add(link.Sheet);
                        break;

                    // hyperlinks
                    case "alternate":
                    case "canonical":
                    case "archives":
                    case "author":
                    case "first":
                    case "help":
                    case "sidebar":
                    case "tag":
                    case "search":
                    case "index":
                    case "license":
                    case "up":
                    case "next":
                    case "last":
                    case "prev":
                        break;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an event of the type specified. (NOT IMPLEMENTED YET)
        /// </summary>
        /// <param name="type">A string that represents the type of event to be created.</param>
        /// <returns>The created Event object.</returns>
        [DOM("createEvent")]
        public Event CreateEvent(String type)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a new Range object. (NOT IMPLEMENTED YET)
        /// </summary>
        /// <returns>The created range object.</returns>
        [DOM("createRange")]
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
        [DOM("prepend")]
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
        [DOM("append")]
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
        [DOM("importNode")]
        public Node ImportNode(Node externalNode, Boolean deep = true)
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
        [DOM("createAttribute")]
        public Attr CreateAttribute(String name)
        {
            return new Attr(name) { OwnerDocument = this };
        }

        /// <summary>
        /// Creates a new attribute node with a namespace, and returns it.
        /// </summary>
        /// <param name="namespaceURI">Specifies the namespace URI to associate with the attribute.</param>
        /// <param name="name">A string containing the name of the attribute.</param>
        /// <returns>The attribute node.</returns>
        [DOM("createAttributeNS")]
        public Attr CreateAttributeNS(String namespaceURI, String name)
        {
            return new Attr(name, string.Empty, namespaceURI) { OwnerDocument = this };
        }

        /// <summary>
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element object.</returns>
        [DOM("createElement")]
        public virtual Element CreateElement(String tagName)
        {
            return new Element { NodeName = tagName, OwnerDocument = this };
        }

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceURI">Specifies the namespace URI to associate with the element.</param>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element.</returns>
        [DOM("createElementNS")]
        public Element CreateElementNS(String namespaceURI, String tagName)
        {
            Element element = null;

            if (namespaceURI == Namespaces.Html)
                element = HTMLElement.Factory(tagName);
            else if (namespaceURI == Namespaces.Svg)
                element = SVGElement.Create(tagName);
            else if (namespaceURI == Namespaces.MathML)
                element = MathMLElement.Create(tagName);
            else if (namespaceURI == Namespaces.Xml)
                element = XMLElement.Create(tagName);
            else
                element = new Element { NamespaceURI = namespaceURI, NodeName = tagName };

            element.OwnerDocument = this;
            return element;
        }

        /// <summary>
        /// Creates a new CDATA section node, and returns it.
        /// </summary>
        /// <param name="data">A string containing the data to be added to the CDATA Section.</param>
        /// <returns></returns>
        [DOM("createCDATASection")]
        public virtual CDATASection CreateCDATASection(String data)
        {
            return new CDATASection { Data = data, OwnerDocument = this };
        }

        /// <summary>
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">A string containing the data to be added to the Comment.</param>
        /// <returns></returns>
        [DOM("createComment")]
        public Comment CreateComment(String data)
        {
            if (data.Contains("--"))
                throw new DOMException(ErrorCode.InvalidCharacter);

            return new Comment(data) { OwnerDocument = this };
        }

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>A new document fragment.</returns>
        [DOM("createDocumentFragment")]
        public DocumentFragment CreateDocumentFragment()
        {
            return new DocumentFragment() { OwnerDocument = this };
        }

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and data strings.
        /// </summary>
        /// <param name="target">The target part of the processing instruction.</param>
        /// <param name="data">The data for the node.</param>
        /// <returns>A new processing instruction.</returns>
        [DOM("createProcessingInstruction")]
        public ProcessingInstruction CreateProcessingInstruction(String target, String data)
        {
            return new ProcessingInstruction { Target = target, Data = data, OwnerDocument = this };
        }

        /// <summary>
        /// Creates an EntityReference object. In addition, if the referenced entity is known,
        /// the child list of the EntityReference node is made the same as that of the corresponding
        /// Entity node.
        /// </summary>
        /// <param name="name">The name of the entity to reference.</param>
        /// <returns>The new EntityReference object.</returns>
        [DOM("createEntityReference")]
        public EntityReference CreateEntityReference(String name)
        {
            return new EntityReference(name) { OwnerDocument = this };
        }

        /// <summary>
        /// Creates a new Text node.
        /// </summary>
        /// <param name="data">A string containing the data to be put in the text node.</param>
        /// <returns>The created Text node.</returns>
        [DOM("createTextNode")]
        public TextNode CreateTextNode(String data)
        {
            return new TextNode(data) { OwnerDocument = this };
        }

        /// <summary>
        /// Returns the Element whose ID is given by elementId. If no such element exists, returns null.
        /// The behavior is not defined if more than one element have this ID.
        /// </summary>
        /// <param name="elementId">A case-sensitive string representing the unique ID of the element being sought.</param>
        /// <returns>The matching element.</returns>
        [DOM("getElementById")]
        public Element GetElementById(String elementId)
        {
            return GetElementById(_children, elementId);
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        [DOM("querySelector")]
        public Element QuerySelector(String selectors)
        {
            return _children.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A list of nodes.</returns>
        [DOM("querySelectorAll")]
        public HTMLCollection QuerySelectorAll(String selectors)
        {
            return _children.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of elements.</returns>
        [DOM("getElementsByClassName")]
        public HTMLCollection GetElementsByClassName(String classNames)
        {
            return _children.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        [DOM("getElementsByTagName")]
        public HTMLCollection GetElementsByTagName(String tagName)
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
        [DOM("getElementsByTagNameNS")]
        public HTMLCollection GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return _children.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
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
        [DOM("lookupNamespaceURI")]
        public override String LookupNamespaceURI(String prefix)
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
        [DOM("lookupPrefix")]
        public override String LookupPrefix(String namespaceURI)
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
        [DOM("isDefaultNamespace")]
        public override Boolean IsDefaultNamespace(String namespaceURI)
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
        [DOM("normalize")]
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
        public override String ToHtml()
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
            target._ready = source._ready;
            target._referrer = source._referrer;
            target._location = source._location;
            target._implementation = source._implementation;
            target._quirksMode = source._quirksMode;
        }

        #endregion
    }
}

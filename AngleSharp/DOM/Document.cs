namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using AngleSharp.DOM.Mathml;
    using AngleSharp.DOM.Svg;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a document node.
    /// </summary>
    public class Document : Node, IDocument, IDocumentStyle
    {
        #region Fields

        QuirksMode _quirksMode;
        Readiness _ready;
        DOMImplementation _implementation;
        IConfiguration _options;
        String _encoding;
        String _originalEncoding;
        DOMStringList _styles;
        StyleSheetList _styleSheets;
        String _referrer;
        
        /// <summary>
        /// The content type of the MIME type from the header.
        /// </summary>
        protected String _contentType;
        /// <summary>
        /// The location of the document.
        /// </summary>
        protected Location _location;

        #endregion

        #region Events

        /// <summary>
        /// This event is fired when the ready state of the document changes.
        /// </summary>
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
            _styleSheets = new StyleSheetList(this);
            _quirksMode = QuirksMode.Off;
            _location = new Location("file://localhost/");
            _options = Configuration.Default;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of child elements.
        /// </summary>
        public Int32 ChildElementCount
        {
            get { return _children.OfType<Element>().Count(); }
        }

        /// <summary>
        /// Gets the child elements.
        /// </summary>
        public HTMLCollection Children
        {
            get { return new HTMLCollection(_children.OfType<Element>()); }
        }

        /// <summary>
        /// Gets the first child element of this element.
        /// </summary>
        public IElement FirstElementChild
        {
            get
            {
                var n = _children.Length;

                for (int i = 0; i < n; i++)
                {
                    if (_children[i] is Element)
                        return (Element)_children[i];
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
                for (int i = _children.Length - 1; i >= 0; i--)
                {
                    if (_children[i] is Element)
                        return (Element)_children[i];
                }

                return null;
            }
        }

        /// <summary>
        /// Gets an indicator if loading the document should be asynchronous or synchronous.
        /// </summary>
        [DomName("async")]
        public Boolean Async
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the DOMImplementation object that handles this document.
        /// </summary>
        public IImplementation Implementation
        {
            get { return _implementation ?? (_implementation = new DOMImplementation()); }
        }

        /// <summary>
        /// Gets a string containing the date and time on which the current document was last modified.
        /// </summary>
        [DomName("lastModified")]
        public DateTime LastModified
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        public IDocumentType Doctype
        {
            get { return FindChild<DocumentType>(this); }
        }

        /// <summary>
        /// Gets the Content-Type from the MIME Header of the current document.
        /// </summary>
        public String ContentType
        {
            get { return _contentType; }
        }

        /// <summary>
        /// Gets or sets the ready state of the document.
        /// </summary>
        [DomName("readyState")]
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
        [DomName("styleSheets")]
        public StyleSheetList StyleSheets
        {
            get { return _styleSheets; }
        }

        /// <summary>
        /// Gets a live list of all of the currently-available style sheet sets.
        /// </summary>
        [DomName("styleSheetSets")]
        public DOMStringList StyleSheetSets
        {
            get { return _styles ?? (_styles = new DOMStringList(_styleSheets.Select(m => m.Title))); }
        }

        /// <summary>
        /// Gets the URI of the page that linked to this page.
        /// </summary>
        [DomName("referrer")]
        public String Referrer
        {
            get { return _referrer; }
            internal protected set { _referrer = value; }
        }

        /// <summary>
        /// Gets or sets the URI of the current document.
        /// </summary>
        [DomName("location")]
        public Location Location
        {
            get { return _location; }
            set { ReLoad(value); }
        }

        /// <summary>
        /// Gets the URI of the current document.
        /// </summary>
        public String DocumentUri
        {
            get { return _location.Href; }
            internal set { _location.Href = value; }
        }

        /// <summary>
        /// Gets the window object associated with the document or null if none available.
        /// </summary>
        [DomName("defaultView")]
        public IWindow DefaultView 
        {
            get; //TODO
            internal set; 
        }

        /// <summary>
        /// Gets the parent window object if any.
        /// </summary>
        [DomName("parentWindow")]
        public IWindow ParentWindow
        {
            get { return DefaultView; }
        }

        /// <summary>
        /// Gets the character encoding of the current document.
        /// </summary>
        public String CharacterSet
        {
            get { return _encoding ?? _originalEncoding; }
            internal set { _encoding = value; }
        }

        /// <summary>
        /// Gets the encoding that was used when the document was parsed.
        /// </summary>
        [DomName("inputEncoding")]
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
        public IElement DocumentElement
        {
            get { return FindChild<Element>(this); }
        }

        /// <summary>
        /// Gets the currently focused element, that is, the element that will get keystroke events if the user types any.
        /// </summary>
        [DomName("activeElement")]
        public IElement ActiveElement 
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a value to indicate whether the document is rendered in Quirks mode (BackComp) 
        /// or Strict mode (CSS1Compat).
        /// </summary>
        public String CompatMode
        {
            get { return QuirksMode == QuirksMode.On ? "BackCompat" : "CSS1Compat"; }
        }

        /// <summary>
        /// Gets a string containing the URL of the current document.
        /// </summary>
        public String Url
        {
            get { return DocumentUri; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets or sets the options to use.
        /// </summary>
        internal IConfiguration Options
        {
            get { return _options; }
            set { _options = value ?? Configuration.Default; }
        }

        /// <summary>
        /// Gets or sets the status of the quirks mode of the document.
        /// </summary>
        internal QuirksMode QuirksMode
        {
            get { return _quirksMode; }
            set { _quirksMode = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an event of the type specified. (NOT IMPLEMENTED YET)
        /// </summary>
        /// <param name="type">A string that represents the type of event to be created.</param>
        /// <returns>The created Event object.</returns>
        [DomName("createEvent")]
        public IEvent CreateEvent(String type)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a new Range object. (NOT IMPLEMENTED YET)
        /// </summary>
        /// <returns>The created range object.</returns>
        [DomName("createRange")]
        public IRange CreateRange()
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prepends nodes to the current document.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        public void Prepend(params INode[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                InsertChild(0, node);
            }
        }

        /// <summary>
        /// Appends nodes to current document.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        public void Append(params INode[] nodes)
        {
            if (_parent != null && nodes.Length > 0)
            {
                var node = MutationMacro(nodes);
                AppendChild(node);
            }
        }

        /// <summary>
        /// Creates a copy of a node from an external document that can be inserted into the current document.
        /// </summary>
        /// <param name="externalNode">The node from another document to be imported.</param>
        /// <param name="deep">Optional argument, indicating whether the descendants of the imported
        /// node need to be imported.</param>
        /// <returns>The new node that is imported into the document. The new node's parentNode is null,
        /// since it has not yet been inserted into the document tree.</returns>
        [DomName("importNode")]
        public Node ImportNode(Node externalNode, Boolean deep = true)
        {
            var clone = externalNode.Clone(deep);
            externalNode.Owner = this;
            return externalNode;
        }

        /// <summary>
        /// Creates a new attribute node, and returns it.
        /// </summary>
        /// <param name="name">A string containing the name of the attribute.</param>
        /// <returns>The attribute node.</returns>
        [DomName("createAttribute")]
        public Attr CreateAttribute(String name)
        {
            return new Attr(name);
        }

        /// <summary>
        /// Creates a new attribute node with a namespace, and returns it.
        /// </summary>
        /// <param name="namespaceURI">Specifies the namespace URI to associate with the attribute.</param>
        /// <param name="name">A string containing the name of the attribute.</param>
        /// <returns>The attribute node.</returns>
        [DomName("createAttributeNS")]
        public Attr CreateAttributeNS(String namespaceURI, String name)
        {
            return new Attr(name, String.Empty, namespaceURI);
        }

        /// <summary>
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element object.</returns>
        [DomName("createElement")]
        public virtual Element CreateElement(String tagName)
        {
            return new Element { NodeName = tagName, Owner = this };
        }

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceURI">Specifies the namespace URI to associate with the element.</param>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element.</returns>
        [DomName("createElementNS")]
        public Element CreateElementNS(String namespaceURI, String tagName)
        {
            Element element = null;

            if (namespaceURI == Namespaces.Html)
                element = HTMLFactory.Create(tagName, this);
            else if (namespaceURI == Namespaces.Svg)
                element = SVGFactory.Create(tagName, this);
            else if (namespaceURI == Namespaces.MathML)
                element = MathFactory.Create(tagName, this);
            else
                element = new Element { NamespaceUri = namespaceURI, NodeName = tagName, Owner = this };

            return element;
        }

        /// <summary>
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">A string containing the data to be added to the Comment.</param>
        /// <returns></returns>
        [DomName("createComment")]
        public IComment CreateComment(String data)
        {
            if (data.Contains("--"))
                throw new DOMException(ErrorCode.InvalidCharacter);

            return new Comment(data) { Owner = this };
        }

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>A new document fragment.</returns>
        [DomName("createDocumentFragment")]
        public IDocumentFragment CreateDocumentFragment()
        {
            return new DocumentFragment() { Owner = this };
        }

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and data strings.
        /// </summary>
        /// <param name="target">The target part of the processing instruction.</param>
        /// <param name="data">The data for the node.</param>
        /// <returns>A new processing instruction.</returns>
        [DomName("createProcessingInstruction")]
        public IProcessingInstruction CreateProcessingInstruction(String target, String data)
        {
            return new ProcessingInstruction(target) { Data = data, Owner = this };
        }

        /// <summary>
        /// Creates an EntityReference object. In addition, if the referenced entity is known,
        /// the child list of the EntityReference node is made the same as that of the corresponding
        /// Entity node.
        /// </summary>
        /// <param name="name">The name of the entity to reference.</param>
        /// <returns>The new EntityReference object.</returns>
        [DomName("createEntityReference")]
        public EntityReference CreateEntityReference(String name)
        {
            return new EntityReference(name) { Owner = this };
        }

        /// <summary>
        /// Creates a new Text node.
        /// </summary>
        /// <param name="data">A string containing the data to be put in the text node.</param>
        /// <returns>The created Text node.</returns>
        [DomName("createTextNode")]
        public IText CreateTextNode(String data)
        {
            return new TextNode(data) { Owner = this };
        }

        /// <summary>
        /// Returns the Element whose ID is given by elementId. If no such element exists, returns null.
        /// The behavior is not defined if more than one element have this ID.
        /// </summary>
        /// <param name="elementId">A case-sensitive string representing the unique ID of the element being sought.</param>
        /// <returns>The matching element.</returns>
        [DomName("getElementById")]
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
        [DomName("querySelector")]
        public IElement QuerySelector(String selectors)
        {
            return _children.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A list of nodes.</returns>
        [DomName("querySelectorAll")]
        public HTMLCollection QuerySelectorAll(String selectors)
        {
            return _children.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of elements.</returns>
        [DomName("getElementsByClassName")]
        public HTMLCollection GetElementsByClassName(String classNames)
        {
            return _children.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        [DomName("getElementsByTagName")]
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
        [DomName("getElementsByTagNameNS")]
        public HTMLCollection GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return _children.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DomName("cloneNode")]
        public override Node Clone(Boolean deep = true)
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
        [DomName("lookupNamespaceURI")]
        public override String LookupNamespaceUri(String prefix)
        {
            var root = DocumentElement;

            if (root != null)
                return root.LookupNamespaceUri(prefix);

            return null;
        }

        /// <summary>
        /// Returns the prefix for a given namespaceURI if present, and null if not. When multiple prefixes are possible,
        /// the result is implementation-dependent.
        /// </summary>
        /// <param name="namespaceURI">The namespaceURI to lookup.</param>
        /// <returns>The prefix.</returns>
        [DomName("lookupPrefix")]
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
        [DomName("isDefaultNamespace")]
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
        [DomName("normalize")]
        public override void Normalize()
        {
            for (int i = 0; i < _children.Length; i++)
            {
                _children[i].Normalize();

                if (_children[i] is Element)
                    ((Element)_children[i]).NormalizeNamespaces();
            }
        }

        #endregion

        #region Internal methods

        internal virtual DocumentFragment Fragment(String value)
        {
            return new DocumentFragment();
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
        /// Reloads the document witht he given location.
        /// </summary>
        /// <param name="url">The value for reloading.</param>
        protected virtual void ReLoad(Location url)
        {
            _location = url;
        }

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
        static protected Element GetElementById(NodeList children, String id)
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
        static protected void CopyDocumentProperties(Document source, Document target, Boolean deep)
        {
            target._ready = source._ready;
            target._referrer = source._referrer;
            target._location.Href = source._location.Href;
            target._implementation = source._implementation;
            target._quirksMode = source._quirksMode;
            target._options = source._options;
        }

        #endregion
    }

    class XmlDocument : Document, IXmlDocument
    { }
}

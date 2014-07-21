namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using AngleSharp.DOM.Mathml;
    using AngleSharp.DOM.Svg;
    using AngleSharp.Parser;
    using AngleSharp.Parser.Html;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node.
    /// </summary>
    public class Document : Node, IDocument, IDocumentStyle
    {
        #region Fields

        Task _queue;
        QuirksMode _quirksMode;
        DocumentReadyState _ready;
        DOMImplementation _implementation;
        IConfiguration _options;
        String _encoding;
        DOMStringList _styles;
        StyleSheetList _styleSheets;
        String _referrer;
        String _cookie;
        HTMLCollection _all;
        HTMLCollection<IHtmlAnchorElement> _anchors;
        HTMLCollection<IHtmlFormElement> _forms;
        HTMLCollection<HTMLScriptElement> _scripts;
        HTMLCollection<IHtmlImageElement> _images;
        HTMLCollection _embeds;
        HTMLCollection _links;
        
        /// <summary>
        /// The content type of the MIME type from the header.
        /// </summary>
        protected String _contentType;
        /// <summary>
        /// The location of the document.
        /// </summary>
        protected ILocation _location;

        #endregion

        #region Events

        public event EventListener ReadyStateChanged;

        public event EventListener Aborted;

        public event EventListener Blurred;

        public event EventListener Cancelled;

        public event EventListener CanPlay;

        public event EventListener CanPlayThrough;

        public event EventListener Changed;

        public event EventListener Clicked;

        public event EventListener CueChanged;

        public event EventListener DoubleClick;

        public event EventListener Drag;

        public event EventListener DragEnd;

        public event EventListener DragEnter;

        public event EventListener DragExit;

        public event EventListener DragLeave;

        public event EventListener DragOver;

        public event EventListener DragStart;

        public event EventListener Dropped;

        public event EventListener DurationChanged;

        public event EventListener Emptied;

        public event EventListener Ended;

        public event ErrorEventListener Error;

        public event EventListener Focused;

        public event EventListener Input;

        public event EventListener Invalid;

        public event EventListener KeyDown;

        public event EventListener KeyPress;

        public event EventListener KeyUp;

        public event EventListener Loaded;

        public event EventListener LoadedData;

        public event EventListener LoadedMetadata;

        public event EventListener Loading;

        public event EventListener MouseDown;

        public event EventListener MouseEnter;

        public event EventListener MouseLeave;

        public event EventListener MouseMove;

        public event EventListener MouseOut;

        public event EventListener MouseOver;

        public event EventListener MouseUp;

        public event EventListener MouseWheel;

        public event EventListener Paused;

        public event EventListener Played;

        public event EventListener Playing;

        public event EventListener Progress;

        public event EventListener RateChanged;

        public event EventListener Resetted;

        public event EventListener Resized;

        public event EventListener Scrolled;

        public event EventListener Seeked;

        public event EventListener Seeking;

        public event EventListener Selected;

        public event EventListener Shown;

        public event EventListener Stalled;

        public event EventListener Submitted;

        public event EventListener Suspended;

        public event EventListener TimeUpdated;

        public event EventListener Toggled;

        public event EventListener VolumeChanged;

        public event EventListener Waiting;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        internal Document()
        {
            _owner = this;
            _type = NodeType.Document;
            IsAsync = true;
            _encoding = DocumentEncoding.Suggest(System.Globalization.CultureInfo.CurrentCulture.Name).WebName;
            _referrer = String.Empty;
            _ready = DocumentReadyState.Complete;
            _name = "#document";
            _styleSheets = new StyleSheetList(this);
            _quirksMode = QuirksMode.Off;
            _location = new Location("file://localhost/");
            _options = Configuration.Default;
            _all = new HTMLCollection(this);
            _queue = new Task(() => { });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all elements in the document.
        /// </summary>
        public IHtmlCollection All
        {
            get { return _all; }
        }

        /// <summary>
        /// Gets a list of all of the anchors in the document.
        /// </summary>
        public IHtmlCollection Anchors
        {
            get { return _anchors ?? (_anchors = new HTMLCollection<IHtmlAnchorElement>(this, predicate: element => element.Attributes.Any(m => m.Name == AttributeNames.Name))); }
        }

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
        public IHtmlCollection Children
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
        public Boolean IsAsync
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
        public String LastModified
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
        public DocumentReadyState ReadyState
        {
            get { return _ready; }
            internal set
            {
                _ready = value;

                if (ReadyStateChanged != null)
                    ReadyStateChanged(Event.Empty);
            }
        }

        /// <summary>
        /// Gets a list of stylesheet objects for stylesheets explicitly linked into or embedded in a document.
        /// </summary>
        public StyleSheetList StyleSheets
        {
            get { return _styleSheets; }
        }

        /// <summary>
        /// Gets a live list of all of the currently-available style sheet sets.
        /// </summary>
        public IStringList StyleSheetSets
        {
            get { return _styles ?? (_styles = new DOMStringList(_styleSheets.Select(m => m.Title))); }
        }

        /// <summary>
        /// Gets the URI of the page that linked to this page.
        /// </summary>
        public String Referrer
        {
            get { return _referrer; }
            internal protected set { _referrer = value; }
        }

        /// <summary>
        /// Gets or sets the URI of the current document.
        /// </summary>
        public ILocation Location
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
        public IWindowProxy DefaultView 
        {
            get; //TODO
            internal set; 
        }

        /// <summary>
        /// Gets the character encoding of the current document.
        /// </summary>
        public String CharacterSet
        {
            get { return _encoding; }
            internal set { _encoding = value; }
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

        /// <summary>
        /// Gets the forms in the document.
        /// </summary>
        public IHtmlCollection Forms
        {
            get { return _forms ?? (_forms = new HTMLCollection<IHtmlFormElement>(this)); }
        }

        /// <summary>
        /// Gets the images in the document.
        /// </summary>
        public IHtmlCollection Images
        {
            get { return _images ?? (_images = new HTMLCollection<IHtmlImageElement>(this)); }
        }

        /// <summary>
        /// Gets the scripts in the document.
        /// </summary>
        public IHtmlCollection Scripts
        {
            get { return _scripts ?? (_scripts = new HTMLCollection<HTMLScriptElement>(this)); }
        }

        /// <summary>
        /// Gets a list of the embedded OBJECTS within the current document.
        /// </summary>
        public IHtmlCollection Embeds
        {
            get { return _embeds ?? (_embeds = new HTMLCollection(this, predicate: element => element is HTMLEmbedElement || element is HTMLObjectElement || element is HTMLAppletElement)); }
        }

        /// <summary>
        /// Gets a collection of all AREA elements and anchor elements in a document with a value for the href attribute.
        /// </summary>
        public IHtmlCollection Links
        {
            get { return _links ?? (_links = new HTMLCollection(this, predicate: element => (element is HTMLAnchorElement || element is HTMLAreaElement) && element.Attributes.Any(m => m.Name == AttributeNames.Href))); }
        }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        public String Title
        {
            get
            {
                var _title = FindChild<IHtmlTitleElement>(Head);

                if (_title != null)
                    return _title.Text;

                return String.Empty;
            }
            set
            {
                var _title = FindChild<IHtmlTitleElement>(Head);

                if (_title == null)
                {
                    var _documentElement = DocumentElement;

                    if (_documentElement == null)
                    {
                        _documentElement = new HTMLHtmlElement();
                        AppendChild(_documentElement);
                    }

                    var _head = Head;

                    if (_head == null)
                    {
                        _head = new HTMLHeadElement();
                        _documentElement.AppendChild(_head);
                    }

                    _title = new HTMLTitleElement();
                    _head.AppendChild(_title);
                }

                _title.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the head element.
        /// </summary>
        public IHtmlHeadElement Head
        {
            get { return FindChild<IHtmlHeadElement>(DocumentElement); }
        }

        /// <summary>
        /// Gets the body element.
        /// </summary>
        public IHtmlElement Body
        {
            get { return FindChild<IHtmlBodyElement>(DocumentElement); }
            set { if (Body != null) Body.Replace(value); else DocumentElement.AppendChild(value); }
        }

        /// <summary>
        /// Gets or sets the document cookie.
        /// </summary>
        public String Cookie
        {
            get { return _cookie; }
            set { _cookie = value; }
        }

        /// <summary>
        /// Gets the domain portion of the origin of the current document.
        /// </summary>
        public String Domain
        {
            get { return String.IsNullOrEmpty(DocumentUri) ? String.Empty : new Uri(DocumentUri).Host; }
            set { if (_location == null) return; _location.Host = value; }
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

        internal Int32 ScriptsWaiting
        {
            get { return 0; }
        }

        internal Int32 ScriptsAsSoonAsPossible
        {
            get { return 0; }
        }

        internal Boolean IsLoadingDelayed
        {
            get { return false; }
        }

        internal Boolean IsInBrowsingContext
        {
            get { return false; }
        }

        internal Boolean IsToBePrinted
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens a document stream for writing.
        /// </summary>
        public IDocument OpenNew(String type = "text/html", String replace = null)
        {
            //TODO
            return new Document();
        }

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        public void CloseCurrent()
        {
            //TODO
        }

        /// <summary>
        /// Writes text to a document.
        /// </summary>
        /// <param name="content">The text to be written on the document.</param>
        public void Write(String content)
        {
            //TODO
        }

        /// <summary>
        /// Writes a line of text to a document.
        /// </summary>
        /// <param name="content">The text to be written on the document.</param>
        public void WriteLine(String content)
        {
            Write(content + Specification.LineFeed);
        }

        /// <summary>
        /// Returns a list of elements with a given name in the HTML document.
        /// </summary>
        /// <param name="name">The value of the name attribute of the element.</param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection GetElementsByName(String name)
        {
            var result = new List<Element>();
            _children.GetElementsByName(name, result);
            return new HTMLCollection(result);
        }

        /// <summary>
        /// Loads the document content from the given URL.
        /// </summary>
        /// <param name="url">The URL that hosts the HTML content.</param>
        [DomName("load")]
        public void Load(String url)
        {
            Uri uri;
            _location.Href = url;
            Cookie = String.Empty;

            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                throw new ArgumentException("The given URL is not valid as an absolute URL.");

            var task = Options.LoadAsync(uri);

            task.ContinueWith(m =>
            {
                if (m.IsCompleted && !m.IsFaulted)
                    Load(m.Result);
            });
        }

        /// <summary>
        /// Creates a copy of a node from an external document that can be inserted into the current document.
        /// </summary>
        /// <param name="externalNode">The node from another document to be imported.</param>
        /// <param name="deep">Optional argument, indicating whether the descendants of the imported
        /// node need to be imported.</param>
        /// <returns>The new node that is imported into the document. The new node's parentNode is null,
        /// since it has not yet been inserted into the document tree.</returns>
        public INode Import(INode externalNode, Boolean deep = true)
        {
            var clone = externalNode.Clone(deep);
            AppendChild(clone);
            return clone;
        }

        /// <summary>
        /// Removes the node from its original document and places it in this document.
        /// </summary>
        /// <param name="externalNode">The node from another document to be adopted.</param>
        /// <returns>The new node that is imported into the document. The new node's parentNode is null,
        /// since it has not yet been inserted into the document tree.</returns>
        public INode Adopt(INode externalNode)
        {
            if (externalNode.Parent != null)
                externalNode.Parent.RemoveChild(externalNode);

            AppendChild(externalNode);
            return externalNode;
        }

        /// <summary>
        /// Creates an event of the type specified. (NOT IMPLEMENTED YET)
        /// </summary>
        /// <param name="type">A string that represents the type of event to be created.</param>
        /// <returns>The created Event object.</returns>
        public IEvent CreateEvent(String type)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new NodeIterator object.
        /// </summary>
        /// <param name="root">The root node at which to begin the NodeIterator's traversal.</param>
        /// <param name="settings">Indicates which nodes to iterate over.</param>
        /// <param name="filter">An optional callback function for filtering.</param>
        /// <returns>The created node NodeIterator.</returns>
        public INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new TreeWalker object.
        /// </summary>
        /// <param name="root">Is the root Node of this TreeWalker traversal.</param>
        /// <param name="settings">Indicates which nodes to iterate over.</param>
        /// <param name="filter">An optional callback function for filtering.</param>
        /// <returns>The created node TreeWalker.</returns>
        public ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a new Range object. (NOT IMPLEMENTED YET)
        /// </summary>
        /// <returns>The created range object.</returns>
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
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element object.</returns>
        public virtual IElement CreateElement(String tagName)
        {
            return HtmlElementFactory.Create(tagName, this);
        }

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceUri">Specifies the namespace URI to associate with the element.</param>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element.</returns>
        public IElement CreateElementNS(String namespaceUri, String tagName)
        {
            Element element = null;

            if (namespaceUri == Namespaces.Html)
                element = HtmlElementFactory.Create(tagName, this);
            else if (namespaceUri == Namespaces.Svg)
                element = SvgElementFactory.Create(tagName, this);
            else if (namespaceUri == Namespaces.MathML)
                element = MathElementFactory.Create(tagName, this);
            else
                element = new Element { NamespaceUri = namespaceUri, NodeName = tagName, Owner = this };

            return element;
        }

        /// <summary>
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">A string containing the data to be added to the Comment.</param>
        /// <returns>The new comment.</returns>
        public IComment CreateComment(String data)
        {
            if (data.Contains("--"))
                throw new DomException(ErrorCode.InvalidCharacter);

            return new Comment(data) { Owner = this };
        }

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>A new document fragment.</returns>
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
        public IProcessingInstruction CreateProcessingInstruction(String target, String data)
        {
            return new ProcessingInstruction(target) { Data = data, Owner = this };
        }

        /// <summary>
        /// Creates a new Text node.
        /// </summary>
        /// <param name="data">A string containing the data to be put in the text node.</param>
        /// <returns>The created Text node.</returns>
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
        public IElement GetElementById(String elementId)
        {
            return GetElementById(_children, elementId);
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
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
        public IHtmlCollection QuerySelectorAll(String selectors)
        {
            return _children.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of elements.</returns>
        public IHtmlCollection GetElementsByClassName(String classNames)
        {
            return _children.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        public IHtmlCollection GetElementsByTagName(String tagName)
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
        public IHtmlCollection GetElementsByTagNameNS(String namespaceURI, String tagName)
        {
            return _children.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
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
        public override void Normalize()
        {
            for (int i = 0; i < _children.Length; i++)
                _children[i].Normalize();
        }

        #endregion

        #region Static Helpers

        /// <summary>
        /// Loads a HTML document from the given URL.
        /// </summary>
        /// <param name="url">The URL that hosts the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The document with the parsed content.</returns>
        public static IDocument LoadFromUrl(String url, IConfiguration configuration = null)
        {
            var doc = new Document { Options = configuration ?? Configuration.Default };
            doc.Load(url);
            return doc;
        }

        /// <summary>
        /// Loads a HTML document from the given URL.
        /// </summary>
        /// <param name="source">The source code with the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The document with the parsed content.</returns>
        public static IDocument LoadFromSource(String source, IConfiguration configuration = null)
        {
            return DocumentBuilder.Html(source, configuration);
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

        #region Internal methods

        /// <summary>
        /// Firing a simple event named e means that a trusted event with the name e,
        /// which does not bubble (except where otherwise stated) and is not cancelable
        /// (except where otherwise stated), and which uses the Event interface, must
        /// be created and dispatched at the given target.
        /// </summary>
        /// <param name="eventName">The name of the event to be fired.</param>
        void FireSimpleEvent(String eventName)
        {
            //TODO
            //http://www.w3.org/html/wg/drafts/html/master/webappapis.html#fire-a-simple-event
        }

        internal void RunNextScript()
        {
            WaitForReady();
            //TODO Run first script that should be executed when the document is finished parsing
        }

        internal void PerformMicrotaskCheckpoint()
        {
            //TODO
            //IF RUNNING MUTATION OBSERVERS == false
            //1. Let the running mutation observers flag be true.
            //2. Sort the tables with pending sorts.
            //3. Invoke MutationObserver objects for the unit of related similar-origin browsing contexts to which the script's browsing context belongs.
            //   ( Note: This will typically invoke scripted callbacks, which calls the jump to a code entry-point algorithm, which calls this perform a )
            //   ( microtask checkpoint algorithm again, which is why we use the running mutation observers flag to avoid reentrancy.                    )
            //4. Let the running mutation observers flag be false.
        }

        internal void ProvideStableState()
        {
            //TODO
            //When the user agent is to provide a stable state, if any asynchronously-running algorithms are awaiting a stable state, then
            //the user agent must run their synchronous section and then resume running their asynchronous algorithm (if appropriate).
        }

        internal void WaitForReady()
        {
            //TODO
            //If the parser's Document has a style sheet that is blocking scripts or the script's "ready to be parser-executed"
            //flag is not set: spin the event loop until the parser's Document has no style sheet that is blocking scripts and
            //the script's "ready to be parser-executed" flag is set.
        }

        internal void RaiseDomContentLoaded()
        {
            FireSimpleEvent(EventNames.DomContentLoaded);
        }

        internal void RaiseLoadedEvent()
        {
            ReadyState = DocumentReadyState.Complete;
            FireSimpleEvent(EventNames.Load);
        }

        internal void QueueTask(Action action)
        {
            _queue = _queue.ContinueWith(_ => action());
        }

        internal void Print()
        {
            //TODO
            //Run the printing steps.
        }

        internal void ShowPage()
        {
            //TODO
            //1. If the Document's page showing flag is true, then abort this task (i.e. don't fire the event below).
            //2. Set the Document's page showing flag to true.
            //3. Fire a trusted event with the name pageshow at the Window object of the Document, but with its target set to the Document object (and the currentTarget set
            //   to the Window object), using the PageTransitionEvent interface, with the persisted attribute initialized to false. This event must not bubble, must not be
            //   cancelable, and has no default action.
        }

        internal void EmptyAppCache()
        {
            //TODO
            //If the Document has any pending application cache download process tasks, then queue each such task in the order they were added to the list of pending
            //application cache download process tasks, and then empty the list of pending application cache download process tasks. The task source for these tasks is
            //the networking task source.
        }

        internal void FinishLoading()
        {
            //TODO
            //The Document is now ready for post-load tasks.
            //Mark the Document as completely loaded.
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Destroys the generated DOM, leaving only the document behind.
        /// </summary>
        void Destroy()
        {
            for (int i = _children.Length - 1; i >= 0; i--)
                RemoveChild(_children[i]);
        }

        /// <summary>
        /// Loads the document content from the given stream.
        /// </summary>
        /// <param name="stream">The stream that contains the HTML content.</param>
        internal void Load(Stream stream)
        {
            ReadyState = DocumentReadyState.Loading;
            var source = new SourceManager(stream, Options.DefaultEncoding());
            Destroy();
            var parser = new HtmlParser(this, source);
            parser.Parse();
        }

        /// <summary>
        /// Reloads the document witht he given location.
        /// </summary>
        /// <param name="url">The value for reloading.</param>
        protected void ReLoad(ILocation url)
        {
            _location = url;
            Load(url.Href);
        }

        /// <summary>
        /// Tries to find a direct child of a certain type.
        /// </summary>
        /// <param name="parent">The parent that contains the elements.</param>
        /// <typeparam name="T">The node type to find.</typeparam>
        /// <returns>The instance or null.</returns>
        protected static T FindChild<T>(INode parent) where T : class, INode
        {
            if (parent == null)
                return null;

            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as T;

                if (child != null)
                    return child;
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
}

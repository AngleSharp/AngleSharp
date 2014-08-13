namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Events;
    using AngleSharp.DOM.Html;
    using AngleSharp.DOM.Mathml;
    using AngleSharp.DOM.Svg;
    using AngleSharp.Parser.Html;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node.
    /// </summary>
    class Document : Node, IDocument
    {
        #region Fields

        readonly StyleSheetList _styleSheets;
        readonly List<HTMLScriptElement> _scripts;

        Task _queue;
        QuirksMode _quirksMode;
        DocumentReadyState _ready;
        IConfiguration _options;
        ITextSource _source;
        String _referrer;
        String _cookie;
        String _contentType;
        ILocation _location;

        #endregion

        #region Events

        public event EventListener ReadyStateChanged
        {
            add { AddEventListener(EventNames.ReadyStateChanged, value); }
            remove { RemoveEventListener(EventNames.ReadyStateChanged, value); }
        }

        public event EventListener Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        public event EventListener Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        public event EventListener Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        public event EventListener CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        public event EventListener CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        public event EventListener Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        public event EventListener Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        public event EventListener CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        public event EventListener DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        public event EventListener Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        public event EventListener DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        public event EventListener DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        public event EventListener DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        public event EventListener DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        public event EventListener DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        public event EventListener DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        public event EventListener Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        public event EventListener DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        public event EventListener Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        public event EventListener Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        public event EventListener Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        public event EventListener Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        public event EventListener Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        public event EventListener Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        public event EventListener KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        public event EventListener KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        public event EventListener KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        public event EventListener Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        public event EventListener LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        public event EventListener LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        public event EventListener Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        public event EventListener MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        public event EventListener MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        public event EventListener MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        public event EventListener MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        public event EventListener MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        public event EventListener MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        public event EventListener MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        public event EventListener MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        public event EventListener Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        public event EventListener Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        public event EventListener Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        public event EventListener Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        public event EventListener RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        public event EventListener Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        public event EventListener Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        public event EventListener Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        public event EventListener Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        public event EventListener Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        public event EventListener Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        public event EventListener Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        public event EventListener Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        public event EventListener Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        public event EventListener Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        public event EventListener TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        public event EventListener Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        public event EventListener VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        public event EventListener Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        internal Document()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        /// <param name="source">The HTML source code.</param>
        internal Document(String source)
            : this(new TextSource(source))
        {
        }

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        /// <param name="source">The underlying source.</param>
        internal Document(ITextSource source)
            : base("#document", NodeType.Document)
        {
            Owner = this;
            IsAsync = true;
            _source = source;
            _referrer = String.Empty;
            _ready = DocumentReadyState.Loading;
            _styleSheets = new StyleSheetList(this);
            _scripts = new List<HTMLScriptElement>();
            _quirksMode = QuirksMode.Off;
            _location = new Location("file://localhost/");
            _options = Configuration.Default;
            _queue = Task.Factory.StartNew(() => { });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all elements in the document.
        /// </summary>
        public IHtmlAllCollection All
        {
            get { return new HtmlAllCollection(this); }
        }

        /// <summary>
        /// Gets a list of all of the anchors in the document.
        /// </summary>
        public IHtmlCollection Anchors
        {
            get { return new HtmlCollection<IHtmlAnchorElement>(this, predicate: element => element.Attributes.Any(m => m.Name == AttributeNames.Name)); }
        }

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
        public IHtmlCollection Children
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
            get { return DomImplementation.Instance; }
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
            protected set { _contentType = value; }
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
                FireSimpleEvent(EventNames.ReadyStateChanged);
            }
        }

        /// <summary>
        /// Gets a list of stylesheet objects for stylesheets explicitly linked into or embedded in a document.
        /// </summary>
        public IStyleSheetList StyleSheets
        {
            get { return _styleSheets; }
        }

        /// <summary>
        /// Gets a live list of all of the currently-available style sheet sets.
        /// </summary>
        public IStringList StyleSheetSets
        {
            get { return new StringList(_styleSheets.Select(m => m.Title)); }
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
            set { LoadHtml(value.Href); }
        }

        /// <summary>
        /// Gets the URI of the current document.
        /// </summary>
        public String DocumentUri
        {
            get { return _location.Href; }
            internal set { _location.Href = value; BaseUri = value; }
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
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        public String Direction
        {
            get { return (DocumentElement as IHtmlElement ?? new HTMLHtmlElement()).Direction; }
            set { (DocumentElement as IHtmlElement ?? new HTMLHtmlElement()).Direction = value; }
        }

        /// <summary>
        /// Gets the character encoding of the current document.
        /// </summary>
        public String CharacterSet
        {
            get { return _source.CurrentEncoding.WebName; }
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
            get { return new HtmlCollection<IHtmlFormElement>(this); }
        }

        /// <summary>
        /// Gets the images in the document.
        /// </summary>
        public IHtmlCollection Images
        {
            get { return new HtmlCollection<IHtmlImageElement>(this); }
        }

        /// <summary>
        /// Gets the scripts in the document.
        /// </summary>
        public IHtmlCollection Scripts
        {
            get { return new HtmlCollection<IHtmlScriptElement>(this); }
        }

        /// <summary>
        /// Gets a list of the embed, applet and object elements within the current document.
        /// </summary>
        public IHtmlCollection Embeds
        {
            get { return new HtmlElementCollection(this, predicate: element => element is HTMLEmbedElement || element is HTMLObjectElement || element is HTMLAppletElement); }
        }

        /// <summary>
        /// Gets a list of the plugin elements within the current document.
        /// </summary>
        public IHtmlCollection Plugins
        {
            get { return new HtmlCollection<IHtmlEmbedElement>(this); }
        }

        /// <summary>
        /// Gets a list of the commands (menu item, button, and link elements) within the current document.
        /// </summary>
        public IHtmlCollection Commands
        {
            get { return new HtmlElementCollection(this, predicate: element => element is HTMLMenuItemElement || element is HTMLButtonElement || element is HTMLAnchorElement); }
        }

        /// <summary>
        /// Gets a collection of all AREA elements and anchor elements in a document with a value for the href attribute.
        /// </summary>
        public IHtmlCollection Links
        {
            get { return new HtmlElementCollection(this, predicate: element => (element is HTMLAnchorElement || element is HTMLAreaElement) && element.Attributes.Any(m => m.Name == AttributeNames.Href)); }
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

        /// <summary>
        /// Gets the origin of the document.
        /// </summary>
        public String Origin
        {
            get { return _location.Origin; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the text stream source.
        /// </summary>
        internal ITextSource Source
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets or sets the options to use.
        /// </summary>
        internal IConfiguration Options
        {
            get { return _options ?? Configuration.Default; }
            set { _options = value; }
        }

        /// <summary>
        /// Gets or sets the status of the quirks mode of the document.
        /// </summary>
        internal QuirksMode QuirksMode
        {
            get { return _quirksMode; }
            set { _quirksMode = value; }
        }

        internal void AddScript(HTMLScriptElement script)
        {
            _scripts.Add(script);
        }

        Int32 ScriptsWaiting
        {
            get { return _scripts.Count; }
        }

        Int32 ScriptsAsSoonAsPossible
        {
            get { return 0; }
        }

        Boolean IsLoadingDelayed
        {
            get { return false; }
        }

        Boolean IsInBrowsingContext
        {
            get { return false; }
        }

        Boolean IsToBePrinted
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Opens a document stream for writing.
        /// </summary>
        public IDocument OpenNew(String type = "text/html", String replace = null)
        {
            //TODO
            return new Document(String.Empty);
        }

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        public void CloseCurrent()
        {
            if (ReadyState != DocumentReadyState.Loading)
                return;

            ReadyState = DocumentReadyState.Interactive;

            while (ScriptsWaiting != 0)
                RunNextScript();

            QueueTask(RaiseDomContentLoaded);
            QueueTask(RaiseLoadedEvent);

            if (IsInBrowsingContext)
                QueueTask(ShowPage);

            QueueTask(EmptyAppCache);

            if (IsToBePrinted)
                Print();

            QueueTask(FinishLoading);
        }

        /// <summary>
        /// Writes text to a document.
        /// </summary>
        /// <param name="content">The text to be written on the document.</param>
        public void Write(String content)
        {
            _source.InsertText(content);
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
            var result = new List<IElement>();
            ChildNodes.GetElementsByName(name, result);
            return new HtmlElementCollection(result);
        }

        /// <summary>
        /// Loads the document content from the given URL.
        /// </summary>
        /// <param name="url">The URL that hosts the HTML content.</param>
        public Boolean LoadHtml(String url)
        {
            DocumentUri = url;
            Cookie = String.Empty;
            var task = Options.LoadAsync(new Url(url));

            var result = task.ContinueWith(m =>
            {
                if (m.IsCompleted && !m.IsFaulted)
                {
                    Load(m.Result);
                    return true;
                }

                return false;
            });

            result.Wait();
            return result.Result;
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
            if (externalNode is IDocument)
                throw new DomException(ErrorCode.NotSupported);

            if (externalNode.Parent != null)
                externalNode.Parent.RemoveChild(externalNode);

            var node = externalNode as Node;

            if (node != null)
                node.Owner = this;

            return externalNode;
        }

        /// <summary>
        /// Creates an event of the type specified. (NOT IMPLEMENTED YET)
        /// </summary>
        /// <param name="type">A string that represents the type of event to be created.</param>
        /// <returns>The created Event object.</returns>
        public IEvent CreateEvent(String type)
        {
            return EventFactory.Create(type);
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
            return new NodeIterator(root, settings, filter);
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
            return new TreeWalker(root, settings, filter);
        }

        /// <summary>
        /// Returns a new Range object.
        /// </summary>
        /// <returns>The created range object.</returns>
        public IRange CreateRange()
        {
            return new Range(this);
        }

        /// <summary>
        /// Prepends nodes to the current document.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        public void Prepend(params INode[] nodes)
        {
            if (Parent != null && nodes.Length > 0)
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
            if (Parent != null && nodes.Length > 0)
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
                element = new Element(tagName) { NamespaceUri = namespaceUri, Owner = this };

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
            return ChildNodes.GetElementById(elementId);
        }

        /// <summary>
        /// Returns the first element within the document (using depth-first pre-order traversal
        /// of the document's nodes) that matches the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>An element object.</returns>
        public IElement QuerySelector(String selectors)
        {
            return ChildNodes.QuerySelector(selectors);
        }

        /// <summary>
        /// Returns a list of the elements within the document (using depth-first pre-order traversal
        /// of the document's nodes) that match the specified group of selectors.
        /// </summary>
        /// <param name="selectors">A string containing one or more CSS selectors separated by commas.</param>
        /// <returns>A list of nodes.</returns>
        public IHtmlCollection QuerySelectorAll(String selectors)
        {
            return ChildNodes.QuerySelectorAll(selectors);
        }

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of elements.</returns>
        public IHtmlCollection GetElementsByClassName(String classNames)
        {
            return ChildNodes.GetElementsByClassName(classNames);
        }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        public IHtmlCollection GetElementsByTagName(String tagName)
        {
            return ChildNodes.GetElementsByTagName(tagName);
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
            return ChildNodes.GetElementsByTagNameNS(namespaceURI, tagName);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new Document(String.Empty);//TODO
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
            var children = ChildNodes;

            for (int i = 0; i < children.Length; i++)
                children[i].Normalize();
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

        void RunNextScript()
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

        void RaiseDomContentLoaded()
        {
            FireSimpleEvent(EventNames.DomContentLoaded);
        }

        void RaiseLoadedEvent()
        {
            ReadyState = DocumentReadyState.Complete;
            FireSimpleEvent(EventNames.Load);
        }

        internal void QueueTask(Action action)
        {
            _queue = _queue.ContinueWith(_ => action());
        }

        void Print()
        {
            //TODO
            //Run the printing steps.
        }

        void ShowPage()
        {
            //TODO
            //1. If the Document's page showing flag is true, then abort this task (i.e. don't fire the event below).
            //2. Set the Document's page showing flag to true.
            //3. Fire a trusted event with the name pageshow at the Window object of the Document, but with its target set to the Document object (and the currentTarget set
            //   to the Window object), using the PageTransitionEvent interface, with the persisted attribute initialized to false. This event must not bubble, must not be
            //   cancelable, and has no default action.
        }

        void EmptyAppCache()
        {
            //TODO
            //If the Document has any pending application cache download process tasks, then queue each such task in the order they were added to the list of pending
            //application cache download process tasks, and then empty the list of pending application cache download process tasks. The task source for these tasks is
            //the networking task source.
        }

        void FinishLoading()
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
            var children = ChildNodes;

            for (int i = children.Length - 1; i >= 0; i--)
                RemoveChild(children[i]);
        }

        /// <summary>
        /// Loads the document content from the given stream.
        /// </summary>
        /// <param name="stream">The stream that contains the HTML content.</param>
        internal void Load(Stream stream)
        {
            ReadyState = DocumentReadyState.Loading;
            _source = new TextSource(stream, Options.DefaultEncoding());
            Destroy();
            var parser = new HtmlParser(this);
            parser.Parse();
        }

        /// <summary>
        /// Tries to find a direct child of a certain type.
        /// </summary>
        /// <param name="parent">The parent that contains the elements.</param>
        /// <typeparam name="T">The node type to find.</typeparam>
        /// <returns>The instance or null.</returns>
        protected static T FindChild<T>(INode parent)
            where T : class, INode
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
            target._quirksMode = source._quirksMode;
            target._options = source._options;
        }

        #endregion
    }
}

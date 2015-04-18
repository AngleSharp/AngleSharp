namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;

    /// <summary>
    /// Represents a document node.
    /// </summary>
    [DebuggerStepThrough]
    abstract class Document : Node, IDocument
    {
        #region Fields

        static readonly String AboutBlank = "about:blank";

        readonly StyleSheetList _styleSheets;
        readonly Queue<HtmlScriptElement> _loadingScripts;
        readonly List<WeakReference> _ranges;
        readonly MutationHost _mutations;
        readonly IBrowsingContext _context;
        readonly IWindow _view;
        readonly IResourceLoader _loader;
        readonly Location _location;
        readonly TextSource _source;

        QuirksMode _quirksMode;
        Sandboxes _sandbox;
        Boolean _async;
        Boolean _designMode;
        Boolean _shown;
        Boolean _salvageable;
        Boolean _firedUnload;
        DocumentReadyState _ready;
        String _referrer;
        String _contentType;
        String _lastStyleSheetSet;
        String _preferredStyleSheetSet;
        IElement _focus;
        HtmlAllCollection _all;
        HtmlCollection<IHtmlAnchorElement> _anchors;
        HtmlElementCollection _children;
        DomImplementation _implementation;
        StringList _styleSheetSets;
        HtmlCollection<IHtmlImageElement> _images;
        HtmlCollection<IHtmlScriptElement> _scripts;
        HtmlCollection<IHtmlEmbedElement> _plugins;
        HtmlElementCollection _commands;
        HtmlElementCollection _links;

        #endregion

        #region Events

        public event DomEventHandler ReadyStateChanged
        {
            add { AddEventListener(EventNames.ReadyStateChanged, value); }
            remove { RemoveEventListener(EventNames.ReadyStateChanged, value); }
        }

        public event DomEventHandler Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        public event DomEventHandler Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        public event DomEventHandler Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        public event DomEventHandler CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        public event DomEventHandler CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        public event DomEventHandler Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        public event DomEventHandler Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        public event DomEventHandler CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        public event DomEventHandler DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        public event DomEventHandler Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        public event DomEventHandler DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        public event DomEventHandler DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        public event DomEventHandler DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        public event DomEventHandler DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        public event DomEventHandler DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        public event DomEventHandler DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        public event DomEventHandler Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        public event DomEventHandler DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        public event DomEventHandler Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        public event DomEventHandler Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        public event DomEventHandler Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        public event DomEventHandler Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        public event DomEventHandler Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        public event DomEventHandler Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        public event DomEventHandler KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        public event DomEventHandler KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        public event DomEventHandler KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        public event DomEventHandler Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        public event DomEventHandler LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        public event DomEventHandler LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        public event DomEventHandler Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        public event DomEventHandler MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        public event DomEventHandler MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        public event DomEventHandler MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        public event DomEventHandler MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        public event DomEventHandler MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        public event DomEventHandler MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        public event DomEventHandler MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        public event DomEventHandler MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        public event DomEventHandler Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        public event DomEventHandler Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        public event DomEventHandler Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        public event DomEventHandler Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        public event DomEventHandler RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        public event DomEventHandler Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        public event DomEventHandler Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        public event DomEventHandler Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        public event DomEventHandler Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        public event DomEventHandler Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        public event DomEventHandler Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        public event DomEventHandler Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        public event DomEventHandler Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        public event DomEventHandler Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        public event DomEventHandler Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        public event DomEventHandler TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        public event DomEventHandler Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        public event DomEventHandler VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        public event DomEventHandler Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        internal Document(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        /// <summary>
        /// Creates a new document node.
        /// </summary>
        /// <param name="context">The context of the document.</param>
        /// <param name="source">The underlying source.</param>
        internal Document(IBrowsingContext context, TextSource source)
            : base(null, "#document", NodeType.Document)
        {
            _async = true;
            _context = context ?? BrowsingContext.New();
            _source = source;
            _referrer = String.Empty;
            _contentType = MimeTypes.ApplicationXml;
            _ready = DocumentReadyState.Loading;
            _styleSheets = new StyleSheetList(this);
            _mutations = new MutationHost(this);
            _preferredStyleSheetSet = String.Empty;
            _loadingScripts = new Queue<HtmlScriptElement>();
            _quirksMode = QuirksMode.Off;
            _designMode = false;
            _location = new Location(AboutBlank);
            _location.Changed += LocationChanged;
            _ranges = new List<WeakReference>();
            _view = this.CreateWindow();
            _firedUnload = false;
            _salvageable = true;
            _shown = false;
            _sandbox = Sandboxes.None;
            _loader = this.CreateLoader();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the whole document is editable.
        /// </summary>
        public String DesignMode
        {
            get { return _designMode ? Keywords.On : Keywords.Off; }
            set { _designMode = value.Equals(Keywords.On, StringComparison.OrdinalIgnoreCase); }
        }

        /// <summary>
        /// Gets a list of all elements in the document.
        /// </summary>
        public IHtmlAllCollection All
        {
            get { return _all ?? (_all = new HtmlAllCollection(this)); }
        }

        /// <summary>
        /// Gets a list of all of the anchors in the document.
        /// </summary>
        public IHtmlCollection<IHtmlAnchorElement> Anchors
        {
            get { return _anchors ?? (_anchors = new HtmlCollection<IHtmlAnchorElement>(this, predicate: IsAnchor)); }
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
        public IHtmlCollection<IElement> Children
        {
            get { return _children ?? (_children = new HtmlElementCollection(ChildNodes.OfType<Element>())); }
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
        /// Gets an indicator if loading the document should be asynchronous or
        /// synchronous.
        /// </summary>
        public Boolean IsAsync
        {
            get { return _async; }
        }

        /// <summary>
        /// Gets the script which is currently being processed.
        /// </summary>
        public IHtmlScriptElement CurrentScript
        {
            get { return _loadingScripts.Count > 0 ? _loadingScripts.Peek() : null; }
        }

        /// <summary>
        /// Gets the DOMImplementation object that handles this document.
        /// </summary>
        public IImplementation Implementation
        {
            get { return _implementation ?? (_implementation = new DomImplementation(this)); }
        }

        /// <summary>
        /// Gets a string containing the date and time on which the current
        /// document was last modified.
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
            get { return this.FindChild<DocumentType>(); }
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
            protected set
            {
                _ready = value;
                this.FireSimpleEvent(EventNames.ReadyStateChanged);
            }
        }

        /// <summary>
        /// Gets a list of stylesheet objects for stylesheets explicitly linked
        /// into or embedded in a document.
        /// </summary>
        public IStyleSheetList StyleSheets
        {
            get { return _styleSheets; }
        }

        /// <summary>
        /// Gets a live list of all of the currently-available style sheet
        /// sets.
        /// </summary>
        public IStringList StyleSheetSets
        {
            get { return _styleSheetSets ?? (_styleSheetSets = new StringList(_styleSheets.Select(m => m.Title))); }
        }

        /// <summary>
        /// Gets the URI of the page that linked to this page.
        /// </summary>
        public String Referrer
        {
            get { return _referrer; }
            protected set { _referrer = value; }
        }

        /// <summary>
        /// Gets or sets the URI of the current document.
        /// </summary>
        public ILocation Location
        {
            get { return _location; }
        }

        /// <summary>
        /// Gets the URI of the current document.
        /// </summary>
        public String DocumentUri
        {
            get { return _location.Href; }
            protected set
            {
                _location.Changed -= LocationChanged;
                _location.Href = value;
                _location.Changed += LocationChanged;
            }
        }

        /// <summary>
        /// Gets the url of the current document.
        /// </summary>
        public Url DocumentUrl
        {
            get { return _location.Original; }
        }

        /// <summary>
        /// Gets the window object associated with the document or null if none
        /// available.
        /// </summary>
        public IWindow DefaultView 
        {
            get { return _view; }
        }

        /// <summary>
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        public String Direction
        {
            get { return (DocumentElement as IHtmlElement ?? new HtmlHtmlElement(this)).Direction; }
            set { (DocumentElement as IHtmlElement ?? new HtmlHtmlElement(this)).Direction = value; }
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
        public abstract IElement DocumentElement
        {
            get;
        }

        /// <summary>
        /// Gets the currently focused element, that is, the element that will
        /// get keystroke events if the user types any.
        /// </summary>
        public IElement ActiveElement 
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets a value to indicate whether the document is rendered in Quirks
        /// mode (BackComp) or Strict mode (CSS1Compat).
        /// </summary>
        public String CompatMode
        {
            get { return _quirksMode.GetCompatiblity(); }
        }

        /// <summary>
        /// Gets a string containing the URL of the current document.
        /// </summary>
        public String Url
        {
            get { return _location.Href; }
        }

        /// <summary>
        /// Gets the forms in the document.
        /// </summary>
        public IHtmlCollection<IHtmlFormElement> Forms
        {
            get { return new HtmlCollection<IHtmlFormElement>(this); }
        }

        /// <summary>
        /// Gets the images in the document.
        /// </summary>
        public IHtmlCollection<IHtmlImageElement> Images
        {
            get { return _images ?? (_images = new HtmlCollection<IHtmlImageElement>(this)); }
        }

        /// <summary>
        /// Gets the scripts in the document.
        /// </summary>
        public IHtmlCollection<IHtmlScriptElement> Scripts
        {
            get { return _scripts ?? (_scripts = new HtmlCollection<IHtmlScriptElement>(this)); }
        }

        /// <summary>
        /// Gets a list of the plugin elements within the current document.
        /// </summary>
        public IHtmlCollection<IHtmlEmbedElement> Plugins
        {
            get { return _plugins ?? (_plugins = new HtmlCollection<IHtmlEmbedElement>(this)); }
        }

        /// <summary>
        /// Gets a list of the commands (menu item, button, and link elements)
        /// within the current document.
        /// </summary>
        public IHtmlCollection<IElement> Commands
        {
            get { return _commands ?? (_commands = new HtmlElementCollection(this, predicate: IsCommand)); }
        }

        /// <summary>
        /// Gets a collection of all AREA elements and anchor elements in a
        /// document with a value for the href attribute.
        /// </summary>
        public IHtmlCollection<IElement> Links
        {
            get { return _links ?? (_links = new HtmlElementCollection(this, predicate: IsLink)); }
        }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        public abstract String Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the head element.
        /// </summary>
        public IHtmlHeadElement Head
        {
            get { return DocumentElement.FindChild<IHtmlHeadElement>(); }
        }

        /// <summary>
        /// Gets the body element.
        /// </summary>
        public IHtmlElement Body
        {
            get
            {
                var root = DocumentElement;

                if (root != null)
                {
                    foreach (var child in root.ChildNodes)
                    {
                        var body = child as HtmlBodyElement;

                        if (body != null)
                            return body;

                        var frameset = child as HtmlFrameSetElement;

                        if (frameset != null)
                            return frameset;
                    }
                }

                return null;
            }
            set 
            {
                if (value is IHtmlBodyElement == false && value is HtmlFrameSetElement == false)
                    throw new DomException(DomError.HierarchyRequest);

                var body = Body;

                if (body == value)
                    return;
                
                if (body == null)
                {
                    var root = DocumentElement;

                    if (root == null)
                        throw new DomException(DomError.HierarchyRequest);
                    else
                        root.AppendChild(value); 
                }
                else
                    ReplaceChild(value, body);
            }
        }

        /// <summary>
        /// Gets the browsing context to use.
        /// </summary>
        public IBrowsingContext Context
        {
            get { return _context; }
        }

        /// <summary>
        /// Gets or sets the document cookie.
        /// </summary>
        public String Cookie
        {
            get { return Options.GetCookie(_location.Origin); }
            set { Options.SetCookie(_location.Origin, value); }
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

        /// <summary>
        /// Gets or sets the selected stylesheet set.
        /// </summary>
        public String SelectedStyleSheetSet
        {
            get 
            {
                var enabled = _styleSheets.GetEnabledStyleSheetSets();
                var enabledName = enabled.FirstOrDefault();
                var others = _styleSheets.Where(m => !String.IsNullOrEmpty(m.Title) && !m.IsDisabled);

                if (enabled.Count() == 1 && !others.Any(m => m.Title != enabledName))
                    return enabledName;
                else if (others.Any())
                    return null;

                return String.Empty;
            }
            set
            {
                if (value != null)
                {
                    _styleSheets.EnableStyleSheetSet(value);
                    _lastStyleSheetSet = value;
                }
            }
        }

        /// <summary>
        /// Gets the last enabled style sheet set; this property's value
        /// changes whenever the SelectedStyleSheetSet property is changed.
        /// </summary>
        public String LastStyleSheetSet
        {
            get { return _lastStyleSheetSet; }
        }

        /// <summary>
        /// Gets the preferred style sheet set as set by the page author.
        /// </summary>
        public String PreferredStyleSheetSet
        {
            get { return _preferredStyleSheetSet; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the document's associated ranges.
        /// </summary>
        internal IEnumerable<Range> Ranges
        {
            get { return _ranges.Where(m => m.IsAlive).Select(m => m.Target).OfType<Range>(); }
        }

        /// <summary>
        /// Gets the mutation host.
        /// </summary>
        internal MutationHost Mutations
        {
            get { return _mutations; }
        }

        /// <summary>
        /// Gets the text stream source.
        /// </summary>
        internal TextSource Source
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets the configuration to use.
        /// </summary>
        internal IConfiguration Options
        {
            get { return _context.Configuration; }
        }

        /// <summary>
        /// Gets or sets the status of the quirks mode of the document.
        /// </summary>
        internal QuirksMode QuirksMode
        {
            get { return _quirksMode; }
            set { _quirksMode = value; }
        }

        /// <summary>
        /// Gets or sets the active sandboxing flag of the document.
        /// </summary>
        internal Sandboxes ActiveSandboxing
        {
            get { return _sandbox; }
            set { _sandbox = value; }
        }

        /// <summary>
        /// Adds a script to the queue of scripts to be run.
        /// </summary>
        /// <param name="script"></param>
        internal void AddScript(HtmlScriptElement script)
        {
            _loadingScripts.Enqueue(script);
        }

        /// <summary>
        /// Gets if a browsing context is available.
        /// </summary>
        internal Boolean IsInBrowsingContext
        {
            get { return _context.Active != null; }
        }

        /// <summary>
        /// Gets if the document is about to be printed.
        /// </summary>
        internal Boolean IsToBePrinted
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the element that is currently focused.
        /// </summary>
        internal IElement FocusElement
        {
            get { return _focus; }
        }

        /// <summary>
        /// Gets the resource loader for the document to use.
        /// </summary>
        internal IResourceLoader Loader
        {
            get { return _loader; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Destroys the current document.
        /// </summary>
        public void Dispose()
        {
            //Important to fix #45
            ReplaceAll(null, true);
            _loadingScripts.Clear();
            _source.Dispose();
        }

        /// <summary>
        /// Enables the stylesheets matching the specified name in the current
        /// stylesheet set, and disables all other stylesheets (except those
        /// without a title, which are always enabled).
        /// </summary>
        /// <param name="name">
        /// The name of the stylesheet set to enable.
        /// </param>
        public void EnableStyleSheetsForSet(String name)
        {
            if (name == null)
                return;

            _styleSheets.EnableStyleSheetSet(name);
        }

        /// <summary>
        /// Opens a new window. Basically just uses the current window (if any)
        /// to open another window.
        /// </summary>
        public IWindow OpenNew(String url, String name, String features, String replace = null)
        {
            var view = DefaultView;

            if (view == null)
                throw new DomException(DomError.InvalidAccess);

            return view.Open(url, name, features, replace);
        }

        /// <summary>
        /// Opens a document stream for writing. For information see:
        /// http://www.whatwg.org/specs/web-apps/current-work/#dom-document-open
        /// </summary>
        public IDocument Open(String type = "text/html", String replace = null)
        {
            if (_contentType != MimeTypes.Html)
                throw new DomException(DomError.InvalidState);

            if (IsInBrowsingContext && _context.Active != this)
                return null;

            var shallReplace = Keywords.Replace.Equals(replace, StringComparison.OrdinalIgnoreCase);

            if (_loadingScripts.Count > 0)
                return this;

            if (shallReplace)
                type = MimeTypes.Html;

            var index = type.IndexOf(Symbols.Semicolon);

            if (index >= 0)
                type = type.Substring(0, index);

            type = type.StripLeadingTailingSpaces();
            //TODO further steps needed.
            //see https://html.spec.whatwg.org/multipage/webappapis.html#dom-document-open
            _contentType = type;
            ReplaceAll(null, false);
            return this;
        }

        /// <summary>
        /// Loads the document content from the given url.
        /// </summary>
        /// <param name="url">The url that hosts the content.</param>
        public void Load(String url)
        {
            Location.Href = url;
        }

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        void IDocument.Close()
        {
            if (ReadyState == DocumentReadyState.Loading)
                FinishLoading().Wait();
        }

        /// <summary>
        /// Writes text to a document.
        /// </summary>
        /// <param name="content">
        /// The text to be written on the document.
        /// </param>
        public void Write(String content)
        {
            if (ReadyState == DocumentReadyState.Complete)
            {
                var newDoc = Open();
                newDoc.Write(content ?? String.Empty);
            }
            else
                _source.InsertText(content);
        }

        /// <summary>
        /// Writes a line of text to a document.
        /// </summary>
        /// <param name="content">
        /// The text to be written on the document.
        /// </param>
        public void WriteLine(String content)
        {
            Write(content + Symbols.LineFeed);
        }

        /// <summary>
        /// Returns a list of elements with a given name in the HTML document.
        /// </summary>
        /// <param name="name">
        /// The value of the name attribute of the element.
        /// </param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection<IElement> GetElementsByName(String name)
        {
            var result = new List<IElement>();
            ChildNodes.GetElementsByName(name, result);
            return new HtmlElementCollection(result);
        }

        /// <summary>
        /// Creates a copy of a node from an external document that can be 
        /// inserted into the current document.
        /// </summary>
        /// <param name="externalNode">
        /// The node from another document to be imported.
        /// </param>
        /// <param name="deep">
        /// Optional argument, indicating whether the descendants of the
        /// imported node need to be imported.
        /// </param>
        /// <returns>
        /// The new node that is imported into the document. The new node's
        /// parentNode is null, since it has not yet been inserted into the
        /// document tree.
        /// </returns>
        public INode Import(INode externalNode, Boolean deep = true)
        {
            if (externalNode.NodeType == NodeType.Document)
                throw new DomException(DomError.NotSupported);

            return externalNode.Clone(deep);
        }

        /// <summary>
        /// Removes the node from its original document and places it in this
        /// document.
        /// </summary>
        /// <param name="externalNode">
        /// The node from another document to be adopted.
        /// </param>
        /// <returns>
        /// The new node that is imported into the document. The new node's
        /// parentNode is null, since it has not yet been inserted into the
        /// document tree.
        /// </returns>
        public INode Adopt(INode externalNode)
        {
            if (externalNode.NodeType == NodeType.Document)
                throw new DomException(DomError.NotSupported);

            this.AdoptNode(externalNode);
            return externalNode;
        }

        /// <summary>
        /// Creates an event of the type specified.
        /// </summary>
        /// <param name="type">
        /// A string that represents the type of event to be created.
        /// </param>
        /// <returns>The created Event object.</returns>
        public Event CreateEvent(String type)
        {
            var ev = Factory.Events.Create(type);

            if (ev == null)
                throw new DomException(DomError.NotSupported);

            return ev;
        }

        /// <summary>
        /// Creates a new NodeIterator object.
        /// </summary>
        /// <param name="root">
        /// The root node at which to begin the NodeIterator's traversal.
        /// </param>
        /// <param name="settings">
        /// Indicates which nodes to iterate over.
        /// </param>
        /// <param name="filter">
        /// An optional callback function for filtering.
        /// </param>
        /// <returns>The created node NodeIterator.</returns>
        public INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
        {
            return new NodeIterator(root, settings, filter);
        }

        /// <summary>
        /// Creates a new TreeWalker object.
        /// </summary>
        /// <param name="root">
        /// Is the root Node of this TreeWalker traversal.
        /// </param>
        /// <param name="settings">
        /// Indicates which nodes to iterate over.
        /// </param>
        /// <param name="filter">
        /// An optional callback function for filtering.
        /// </param>
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
            var range = new Range(this);
            _ranges.Add(new WeakReference(range));
            return range;
        }

        /// <summary>
        /// Prepends nodes to the current document.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        public void Prepend(params INode[] nodes)
        {
            this.PrependNodes(nodes);
        }

        /// <summary>
        /// Appends nodes to current document.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        public void Append(params INode[] nodes)
        {
            this.AppendNodes(nodes);
        }

        /// <summary>
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="localName">
        /// A string that specifies the type of element to be created.
        /// </param>
        /// <returns>The created element object.</returns>
        public IElement CreateElement(String localName)
        {
            if (!localName.IsXmlName())
                throw new DomException(DomError.InvalidCharacter);

            return Factory.HtmlElements.Create(this, localName);
        }

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceUri">
        /// Specifies the namespace URI to associate with the element.
        /// </param>
        /// <param name="qualifiedName">
        /// A string that specifies the type of element to be created.
        /// </param>
        /// <returns>The created element.</returns>
        public IElement CreateElement(String namespaceUri, String qualifiedName)
        {
            if (String.IsNullOrEmpty(namespaceUri))
                namespaceUri = null;

            if (!qualifiedName.IsXmlName())
                throw new DomException(DomError.InvalidCharacter);
            else if (!qualifiedName.IsQualifiedName())
                throw new DomException(DomError.Namespace);

            var parts = qualifiedName.Split(':');
            var prefix = parts.Length == 2 ? parts[0] : null;
            var localName = parts.Length == 2 ? parts[1] : qualifiedName;

            if ((prefix == Namespaces.XmlPrefix && namespaceUri != Namespaces.XmlUri) ||
                ((qualifiedName == Namespaces.XmlNsPrefix || prefix == Namespaces.XmlNsPrefix) && namespaceUri != Namespaces.XmlNsUri) ||
                (namespaceUri == Namespaces.XmlNsUri && (qualifiedName != Namespaces.XmlNsPrefix || prefix != Namespaces.XmlNsPrefix)))
                throw new DomException(DomError.Namespace);

            var element = default(Element);

            if (namespaceUri == Namespaces.HtmlUri)
                element = Factory.HtmlElements.Create(this, localName, prefix);
            else if (namespaceUri == Namespaces.SvgUri)
                element = Factory.SvgElements.Create(this, localName, prefix);
            else if (namespaceUri == Namespaces.MathMlUri)
                element = Factory.MathElements.Create(this, localName, prefix);
            else
                element = new Element(this, localName, prefix, namespaceUri);
            
            return element;
        }

        /// <summary>
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">
        /// A string containing the data to be added to the Comment.
        /// </param>
        /// <returns>The new comment.</returns>
        public IComment CreateComment(String data)
        {
            return new Comment(this, data);
        }

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>A new document fragment.</returns>
        public IDocumentFragment CreateDocumentFragment()
        {
            return new DocumentFragment(this);
        }

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and
        /// data strings.
        /// </summary>
        /// <param name="target">
        /// The target part of the processing instruction.
        /// </param>
        /// <param name="data">The data for the node.</param>
        /// <returns>A new processing instruction.</returns>
        public IProcessingInstruction CreateProcessingInstruction(String target, String data)
        {
            if (!target.IsXmlName() || data.Contains("?>"))
                throw new DomException(DomError.InvalidCharacter);

            return new ProcessingInstruction(this, target) { Data = data };
        }

        /// <summary>
        /// Creates a new Text node.
        /// </summary>
        /// <param name="data">
        /// A string containing the data to be put in the text node.
        /// </param>
        /// <returns>The created Text node.</returns>
        public IText CreateTextNode(String data)
        {
            return new TextNode(this, data);
        }

        /// <summary>
        /// Returns the Element whose ID is given by elementId. If no such
        /// element exists, returns null. The behavior is not defined if more
        /// than one element have this ID.
        /// </summary>
        /// <param name="elementId">
        /// A case-sensitive string representing the unique ID of the element
        /// being sought.
        /// </param>
        /// <returns>The matching element.</returns>
        public IElement GetElementById(String elementId)
        {
            return ChildNodes.GetElementById(elementId);
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
        /// <returns>A list of nodes.</returns>
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
        /// <returns>A collection of elements.</returns>
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
        /// A collection of elements in the order they appear in the tree.
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
        /// A collection of elements in the order they appear in the tree.
        /// </returns>
        public IHtmlCollection<IElement> GetElementsByTagName(String namespaceURI, String tagName)
        {
            return ChildNodes.GetElementsByTagName(namespaceURI, tagName);
        }

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">
        /// Optional value: true if the children of the node should also be
        /// cloned, or false to clone only the specified node.
        /// </param>
        /// <returns>The duplicate node.</returns>
        public override abstract INode Clone(Boolean deep = true);

        /// <summary>
        /// Returns an HTML-code representation of the node.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>A string containing the HTML code.</returns>
        public override String ToHtml(IMarkupFormatter formatter)
        {
            return ChildNodes.ToHtml(formatter);
        }

        /// <summary>
        /// Checks if the document has focus.
        /// </summary>
        /// <returns>
        /// True if the document's context is focused and the document is
        /// active, otherwise false.
        /// </returns>
        public Boolean HasFocus()
        {
            return _context.Active == this;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Sets the focus to the provided element.
        /// </summary>
        /// <param name="element">The element to focus on.</param>
        internal void SetFocus(IElement element)
        {
            _focus = element;
        }

        /// <summary>
        /// Checks if the document is waiting for a script to finish preparing.
        /// </summary>
        /// <returns>
        /// True if any script is still preparing, otherwise false.
        /// </returns>
        internal Boolean IsWaitingForScript()
        {
            return _loadingScripts.Count > 0 && _loadingScripts.Peek().IsReady == false;
        }

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        internal async Task FinishLoading()
        {
            ReadyState = DocumentReadyState.Interactive;

            while (_loadingScripts.Count > 0)
            {
                await this.WaitForReady().ConfigureAwait(false);
                _loadingScripts.Dequeue().Run();
            }

            this.QueueTask(RaiseDomContentLoaded);
            this.QueueTask(RaiseLoadedEvent);

            if (IsInBrowsingContext)
                this.QueueTask(ShowPage);

            this.QueueTask(EmptyAppCache);

            if (IsToBePrinted)
                Print();
        }

        /// <summary>
        /// Unloads the document. For more details, see:
        /// http://www.w3.org/html/wg/drafts/html/CR/browsers.html#unload-a-document
        /// </summary>
        /// <param name="recycle">The recycle parameter.</param>
        /// <param name="cancelToken">Token for cancellation.</param>
        /// <returns>The task that unloads the document.</returns>
        internal void Unload(Boolean recycle, CancellationToken cancelToken)
        {
            var window = DefaultView as EventTarget;

            if (_shown)
            {
                _shown = false;
                this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageHide, false, false, _salvageable), window);
            }

            if (!_firedUnload)
                window.FireSimpleEvent(EventNames.Unload);

            this.ReleaseStorageMutex();

            if (window.HasEventListener(EventNames.Unload))
            {
                _firedUnload = true;
                _salvageable = false;
            }

            //TODO cont. at 11.)
        }

        #endregion

        #region Commands

        Boolean IDocument.ExecuteCommand(String commandId, Boolean showUserInterface, String value)
        {
            var command = Options.GetCommand(commandId);

            if (command != null)
                return command.Execute(this, showUserInterface, value);

            return false;
        }

        Boolean IDocument.IsCommandEnabled(String commandId)
        {
            var command = Options.GetCommand(commandId);

            if (command != null)
                return command.IsEnabled(this);

            return false;
        }

        Boolean IDocument.IsCommandIndeterminate(String commandId)
        {
            var command = Options.GetCommand(commandId);

            if (command != null)
                return command.IsIndeterminate(this);

            return false;
        }

        Boolean IDocument.IsCommandExecuted(String commandId)
        {
            var command = Options.GetCommand(commandId);

            if (command != null)
                return command.IsExecuted(this);

            return false;
        }

        Boolean IDocument.IsCommandSupported(String commandId)
        {
            var command = Options.GetCommand(commandId);

            if (command != null)
                return command.IsSupported(this);

            return false;
        }

        String IDocument.GetCommandValue(String commandId)
        {
            var command = Options.GetCommand(commandId);

            if (command != null)
                return command.GetValue(this);

            return null;
        }

        #endregion

        #region Helpers

        static Boolean IsCommand(IElement element)
        {
            return element is IHtmlMenuItemElement || element is IHtmlButtonElement || element is IHtmlAnchorElement;
        }

        static Boolean IsLink(IElement element)
        {
            return (element is IHtmlAnchorElement || element is IHtmlAreaElement) && element.Attributes.Any(m => m.Name == AttributeNames.Href);
        }

        static Boolean IsAnchor(IHtmlAnchorElement element)
        {
            return element.Attributes.Any(m => m.Name == AttributeNames.Name);
        }

        void RaiseDomContentLoaded()
        {
            this.FireSimpleEvent(EventNames.DomContentLoaded);
        }

        void RaiseLoadedEvent()
        {
            ReadyState = DocumentReadyState.Complete;
            this.FireSimpleEvent(EventNames.Load);
        }

        void EmptyAppCache()
        {
            //TODO
            //If the Document has any pending application cache download
            //process tasks, then queue each such task in the order they were
            //added to the list of pending application cache download process
            //tasks, and then empty the list of pending application cache
            //download process tasks. The task source for these tasks is the
            //networking task source.
        }

        void Print()
        {
            this.FireSimpleEvent(EventNames.BeforePrint);

            //TODO
            //Run the printing steps (such as displaying a save to pdf dialog).
            //http://www.w3.org/html/wg/drafts/html/master/webappapis.html#printing-steps

            this.FireSimpleEvent(EventNames.AfterPrint);
        }

        void ShowPage()
        {
            if (_shown || _view == null)
                return;
            
            _shown = true;
            this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageShow, false, false, false), _view as EventTarget);
        }

        async void LocationChanged(Object sender, Location.LocationChangedEventArgs e)
        {
            if (e.IsHashChanged)
            {
                var ev = new HashChangedEvent();
                ev.Init(EventNames.HashChange, false, false, e.PreviousLocation, e.CurrentLocation);
                ev.IsTrusted = true;
                ev.Dispatch(this);
            }
            else
            {
                var url = new Url(e.CurrentLocation);
                var request = new DocumentRequest(url)
                {
                    Source = this,
                    Referer = DocumentUri
                };

                await _context.OpenAsync(request, CancellationToken.None);
            }
        }

        protected sealed override String LocateNamespace(String prefix)
        {
            return DocumentElement.LocateNamespace(prefix);
        }

        protected sealed override String LocatePrefix(String namespaceUri)
        {
            return DocumentElement.LocatePrefix(namespaceUri);
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
            target._sandbox = source._sandbox;
            target._async = source._async;
            target._contentType = source._contentType;
        }

        #endregion
    }
}

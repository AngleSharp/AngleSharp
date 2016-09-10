namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Mathml;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node.
    /// </summary>
    abstract class Document : Node, IDocument
    {
        #region Fields

        private static readonly ConditionalWeakTable<Document, List<WeakReference>> AttachedReferences = 
            new ConditionalWeakTable<Document, List<WeakReference>>();

        private readonly Queue<HtmlScriptElement> _loadingScripts;
        private readonly MutationHost _mutations;
        private readonly IBrowsingContext _context;
        private readonly IEventLoop _loop;
        private readonly Window _view;
        private readonly IResourceLoader _loader;
        private readonly Location _location;
        private readonly TextSource _source;

        private QuirksMode _quirksMode;
        private Sandboxes _sandbox;
        private Boolean _async;
        private Boolean _designMode;
        private Boolean _shown;
        private Boolean _salvageable;
        private Boolean _firedUnload;
        private DocumentReadyState _ready;
        private IElement _focus;
        private HtmlAllCollection _all;
        private HtmlCollection<IHtmlAnchorElement> _anchors;
        private HtmlCollection<IElement> _children;
        private DomImplementation _implementation;
        private IStringList _styleSheetSets;
        private HtmlCollection<IHtmlImageElement> _images;
        private HtmlCollection<IHtmlScriptElement> _scripts;
        private HtmlCollection<IHtmlEmbedElement> _plugins;
        private HtmlCollection<IElement> _commands;
        private HtmlCollection<IElement> _links;
        private IStyleSheetList _styleSheets;
        private HttpStatusCode _statusCode;

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

        internal Document(IBrowsingContext context, TextSource source)
            : base(null, "#document", NodeType.Document)
        {
            AttachedReferences.Add(this, new List<WeakReference>());
            Referrer = String.Empty;
            ContentType = MimeTypeNames.ApplicationXml;
            _async = true;
            _designMode = false;
            _firedUnload = false;
            _salvageable = true;
            _shown = false;
            _context = context;
            _source = source;
            _ready = DocumentReadyState.Loading;
            _sandbox = Sandboxes.None;
            _quirksMode = QuirksMode.Off;
            _loadingScripts = new Queue<HtmlScriptElement>();
            _location = new Location("about:blank");
            _location.Changed += LocationChanged;
            _view = new Window(this);
            _loader = context.CreateService<IResourceLoader>();
            _loop = context.CreateService<IEventLoop>();
            _mutations = new MutationHost(_loop);
            _statusCode = HttpStatusCode.OK;
        }

        #endregion

        #region Properties

        public TextSource Source
        {
            get { return _source; }
        }

        public IDocument ImportAncestor
        {
            get;
            private set;
        }

        public IEventLoop Loop
        {
            get { return _loop; }
        }

        public String DesignMode
        {
            get { return _designMode ? Keywords.On : Keywords.Off; }
            set { _designMode = value.Isi(Keywords.On); }
        }

        public IHtmlAllCollection All
        {
            get { return _all ?? (_all = new HtmlAllCollection(this)); }
        }

        public IHtmlCollection<IHtmlAnchorElement> Anchors
        {
            get { return _anchors ?? (_anchors = new HtmlCollection<IHtmlAnchorElement>(this, predicate: IsAnchor)); }
        }

        public Int32 ChildElementCount
        {
            get { return ChildNodes.OfType<Element>().Count(); }
        }

        public IHtmlCollection<IElement> Children
        {
            get { return _children ?? (_children = new HtmlCollection<IElement>(ChildNodes.OfType<Element>())); }
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

        public Boolean IsAsync
        {
            get { return _async; }
        }

        public IHtmlScriptElement CurrentScript
        {
            get { return _loadingScripts.Count > 0 ? _loadingScripts.Peek() : null; }
        }

        public IImplementation Implementation
        {
            get { return _implementation ?? (_implementation = new DomImplementation(this)); }
        }

        public String LastModified
        {
            get;
            protected set;
        }

        public IDocumentType Doctype
        {
            get { return this.FindChild<DocumentType>(); }
        }

        public String ContentType
        {
            get;
            protected set;
        }

        public DocumentReadyState ReadyState
        {
            get { return _ready; }
            protected set
            {
                _ready = value;
                this.FireSimpleEvent(EventNames.ReadyStateChanged);
            }
        }

        public IStyleSheetList StyleSheets
        {
            get { return _styleSheets ?? (_styleSheets = this.CreateStyleSheets()); }
        }

        public IStringList StyleSheetSets
        {
            get { return _styleSheetSets ?? (_styleSheetSets = this.CreateStyleSheetSets()); }
        }

        public String Referrer
        {
            get;
            protected set;
        }

        public ILocation Location
        {
            get { return _location; }
        }

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

        public Url DocumentUrl
        {
            get { return _location.Original; }
        }

        public IWindow DefaultView 
        {
            get { return _view; }
        }

        public String Direction
        {
            get { return (DocumentElement as IHtmlElement ?? new HtmlHtmlElement(this)).Direction; }
            set { (DocumentElement as IHtmlElement ?? new HtmlHtmlElement(this)).Direction = value; }
        }

        public String CharacterSet
        {
            get { return _source.CurrentEncoding.WebName; }
        }

        public abstract IElement DocumentElement
        {
            get;
        }

        public IElement ActiveElement 
        {
            get { return All.Where(m => m.IsFocused).FirstOrDefault(); }
        }

        public String CompatMode
        {
            get { return _quirksMode.GetCompatiblity(); }
        }

        public String Url
        {
            get { return _location.Href; }
        }

        public IHtmlCollection<IHtmlFormElement> Forms
        {
            get { return new HtmlCollection<IHtmlFormElement>(this); }
        }

        public IHtmlCollection<IHtmlImageElement> Images
        {
            get { return _images ?? (_images = new HtmlCollection<IHtmlImageElement>(this)); }
        }

        public IHtmlCollection<IHtmlScriptElement> Scripts
        {
            get { return _scripts ?? (_scripts = new HtmlCollection<IHtmlScriptElement>(this)); }
        }

        public IHtmlCollection<IHtmlEmbedElement> Plugins
        {
            get { return _plugins ?? (_plugins = new HtmlCollection<IHtmlEmbedElement>(this)); }
        }

        public IHtmlCollection<IElement> Commands
        {
            get { return _commands ?? (_commands = new HtmlCollection<IElement>(this, predicate: IsCommand)); }
        }

        public IHtmlCollection<IElement> Links
        {
            get { return _links ?? (_links = new HtmlCollection<IElement>(this, predicate: IsLink)); }
        }

        public String Title
        {
            get { return GetTitle(); }
            set { SetTitle(value); }
        }

        public IHtmlHeadElement Head
        {
            get { return DocumentElement.FindChild<IHtmlHeadElement>(); }
        }

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
                        {
                            return body;
                        }

                        var frameset = child as HtmlFrameSetElement;

                        if (frameset != null)
                        {
                            return frameset;
                        }
                    }
                }

                return null;
            }
            set 
            {
                if (value is IHtmlBodyElement == false && value is HtmlFrameSetElement == false)
                    throw new DomException(DomError.HierarchyRequest);

                var body = Body;

                if (body != value)
                {
                    if (body == null)
                    {
                        var root = DocumentElement;

                        if (root == null)
                            throw new DomException(DomError.HierarchyRequest);
                        
                        root.AppendChild(value);
                    }
                    else
                    {
                        ReplaceChild(value, body);
                    }
                }
            }
        }

        public IBrowsingContext Context
        {
            get { return _context; }
        }

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
            private set { _statusCode = value; }
        }

        public String Cookie
        {
            get { return Options.GetCookie(_location.Origin); }
            set { Options.SetCookie(_location.Origin, value); }
        }

        public String Domain
        {
            get { return String.IsNullOrEmpty(DocumentUri) ? String.Empty : new Uri(DocumentUri).Host; }
            set { if (_location == null) return; _location.Host = value; }
        }

        public String Origin
        {
            get { return _location.Origin; }
        }

        public String SelectedStyleSheetSet
        {
            get 
            {
                var enabled = StyleSheets.GetEnabledStyleSheetSets();
                var enabledName = enabled.FirstOrDefault();
                var others = StyleSheets.Where(m => !String.IsNullOrEmpty(m.Title) && !m.IsDisabled);

                if (enabled.Count() == 1 && !others.Any(m => !m.Title.Is(enabledName)))
                {
                    return enabledName;
                }
                else if (others.Any())
                {
                    return null;
                }

                return String.Empty;
            }
            set
            {
                if (value != null)
                {
                    StyleSheets.EnableStyleSheetSet(value);
                    LastStyleSheetSet = value;
                }
            }
        }

        public String LastStyleSheetSet
        {
            get;
            private set;
        }

        public String PreferredStyleSheetSet
        {
            get { return All.OfType<IHtmlLinkElement>().Where(m => m.IsPreferred()).Select(m => m.Title).FirstOrDefault(); }
        }

        public Boolean IsReady
        {
            get { return ReadyState == DocumentReadyState.Complete; }
        }

        public Boolean IsLoading
        {
            get { return ReadyState == DocumentReadyState.Loading; }
        }

        #endregion

        #region Internal Properties

        internal MutationHost Mutations
        {
            get { return _mutations; }
        }

        internal IConfiguration Options
        {
            get { return _context.Configuration; }
        }

        internal QuirksMode QuirksMode
        {
            get { return _quirksMode; }
            set { _quirksMode = value; }
        }

        internal Sandboxes ActiveSandboxing
        {
            get { return _sandbox; }
            set { _sandbox = value; }
        }

        internal void AddScript(HtmlScriptElement script)
        {
            _loadingScripts.Enqueue(script);
        }

        internal Boolean IsInBrowsingContext
        {
            get { return _context.Active != null; }
        }

        internal Boolean IsToBePrinted
        {
            get { return false; }
        }

        internal IElement FocusElement
        {
            get { return _focus; }
        }

        internal IResourceLoader Loader
        {
            get { return _loader; }
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            //Important to fix #45
            ReplaceAll(null, true);
            _loop.CancelAll();
            _loadingScripts.Clear();
            _source.Dispose();
        }

        public void EnableStyleSheetsForSet(String name)
        {
            if (name != null)
            {
                StyleSheets.EnableStyleSheetSet(name);
            }
        }

        public IDocument Open(String type = "text/html", String replace = null)
        {
            if (!ContentType.Is(MimeTypeNames.Html))
                throw new DomException(DomError.InvalidState);

            if (!IsInBrowsingContext || Object.ReferenceEquals(_context.Active, this))
            {
                var responsibleDocument = _context?.Parent.Active;

                if (responsibleDocument != null && !responsibleDocument.Origin.Is(Origin))
                    throw new DomException(DomError.Security);

                if (!_firedUnload && _loadingScripts.Count == 0)
                {
                    var shallReplace = replace.Isi(Keywords.Replace);
                    var history = _context.SessionHistory;
                    var index = type?.IndexOf(Symbols.Semicolon) ?? -1;

                    if (!shallReplace && history != null)
                    {
                        shallReplace = history.Length == 1 && history[0].Url.Is("about:blank");
                    }

                    _salvageable = false;

                    var shouldUnload = PromptToUnloadAsync().Result;

                    if (!shouldUnload)
                    {
                        return this;
                    }
                
                    Unload(recycle: true);
                    Abort();
                    RemoveEventListeners();

                    foreach (var element in this.Descendents<Element>())
                    {
                        element.RemoveEventListeners();
                    }

                    _loop.CancelAll();
                    ReplaceAll(null, suppressObservers: true);
                    _source.CurrentEncoding = TextEncoding.Utf8;
                    _salvageable = true;
                    _ready = DocumentReadyState.Loading;

                    if (type.Isi(Keywords.Replace))
                    {
                        type = MimeTypeNames.Html;
                    }
                    else if (index >= 0)
                    {
                        type = type.Substring(0, index);
                    }

                    type = type.StripLeadingTrailingSpaces();

                    if (!type.Isi(MimeTypeNames.Html))
                    {
                        //Act as if the tokenizer had emitted a start tag token with the tag name "pre" followed by a single
                        //U+000A LINE FEED(LF) character, then switch the HTML parser's tokenizer to the PLAINTEXT state.
                    }

                    ContentType = type;
                    _firedUnload = false;
                    _source.Index = _source.Length;
                }

                return this;
            }

            return null;
        }

        public void Load(String url)
        {
            Location.Href = url;
        }

        void IDocument.Close()
        {
            if (IsLoading)
            {
                FinishLoadingAsync().Wait();
            }
        }

        public void Write(String content)
        {
            if (IsReady)
            {
                var source = content ?? String.Empty;
                var newDocument = Open();
                newDocument.Write(source);
            }
            else
            {
                _source.InsertText(content);
            }
        }

        public void WriteLine(String content)
        {
            Write(content + Symbols.LineFeed);
        }

        public IHtmlCollection<IElement> GetElementsByName(String name)
        {
            var result = new List<IElement>();
            ChildNodes.GetElementsByName(name, result);
            return new HtmlCollection<IElement>(result);
        }

        public INode Import(INode externalNode, Boolean deep = true)
        {
            if (externalNode.NodeType == NodeType.Document)
                throw new DomException(DomError.NotSupported);

            return externalNode.Clone(deep);
        }

        public INode Adopt(INode externalNode)
        {
            if (externalNode.NodeType == NodeType.Document)
                throw new DomException(DomError.NotSupported);

            this.AdoptNode(externalNode);
            return externalNode;
        }

        public Event CreateEvent(String type)
        {
            var factory = Options.GetFactory<IEventFactory>();
            var ev = factory.Create(type);

            if (ev == null)
                throw new DomException(DomError.NotSupported);

            return ev;
        }

        public INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
        {
            return new NodeIterator(root, settings, filter);
        }

        public ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
        {
            return new TreeWalker(root, settings, filter);
        }

        public IRange CreateRange()
        {
            var range = new Range(this);
            AttachReference(range);
            return range;
        }

        public void Prepend(params INode[] nodes)
        {
            this.PrependNodes(nodes);
        }

        public void Append(params INode[] nodes)
        {
            this.AppendNodes(nodes);
        }

        public IElement CreateElement(String localName)
        {
            if (localName.IsXmlName())
            {
                var factory = Options.GetFactory<IElementFactory<HtmlElement>>();
                var element = factory.Create(this, localName);
                element.SetupElement();
                return element;
            }

            throw new DomException(DomError.InvalidCharacter);
        }

        public IElement CreateElement(String namespaceUri, String qualifiedName)
        {
            var localName = default(String);
            var prefix = default(String);
            GetPrefixAndLocalName(qualifiedName, ref namespaceUri, out prefix, out localName);

            if (namespaceUri.Is(NamespaceNames.HtmlUri))
            {
                var factory = Options.GetFactory<IElementFactory<HtmlElement>>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else if (namespaceUri.Is(NamespaceNames.SvgUri))
            {
                var factory = Options.GetFactory<IElementFactory<SvgElement>>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else if (namespaceUri.Is(NamespaceNames.MathMlUri))
            {
                var factory = Options.GetFactory<IElementFactory<MathElement>>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else
            {
                var element = new Element(this, localName, prefix, namespaceUri);
                element.SetupElement();
                return element;
            }
        }

        public IComment CreateComment(String data)
        {
            return new Comment(this, data);
        }

        public IDocumentFragment CreateDocumentFragment()
        {
            return new DocumentFragment(this);
        }

        public IProcessingInstruction CreateProcessingInstruction(String target, String data)
        {
            if (!target.IsXmlName() || data.Contains("?>"))
                throw new DomException(DomError.InvalidCharacter);

            return new ProcessingInstruction(this, target) { Data = data };
        }

        public IText CreateTextNode(String data)
        {
            return new TextNode(this, data);
        }

        public IElement GetElementById(String elementId)
        {
            return ChildNodes.GetElementById(elementId);
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

        public IHtmlCollection<IElement> GetElementsByTagName(String namespaceURI, String tagName)
        {
            return ChildNodes.GetElementsByTagName(namespaceURI, tagName);
        }

        public override abstract INode Clone(Boolean deep = true);

        public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            ChildNodes.ToHtml(writer, formatter);
        }

        public Boolean HasFocus()
        {
            return Object.ReferenceEquals(_context.Active, this);
        }

        public IAttr CreateAttribute(String localName)
        {
            if (!localName.IsXmlName())
                throw new DomException(DomError.InvalidCharacter);

            return new Attr(localName);
        }

        public IAttr CreateAttribute(String namespaceUri, String qualifiedName)
        {
            var localName = default(String);
            var prefix = default(String);
            GetPrefixAndLocalName(qualifiedName, ref namespaceUri, out prefix, out localName);
            return new Attr(prefix, localName, String.Empty, namespaceUri);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the specified attached references.
        /// </summary>
        /// <typeparam name="T">The type of values to get.</typeparam>
        /// <returns>Gets the enumeration over all values.</returns>
        internal IEnumerable<T> GetAttachedReferences<T>()
            where T : class
        {
            var references = default(List<WeakReference>);
            AttachedReferences.TryGetValue(this, out references);
            return references.Select(entry => entry.IsAlive ? entry.Target as T : null).Where(m => m != null);
        }

        /// <summary>
        /// Attaches another reference to this document.
        /// </summary>
        /// <param name="value">The value to attach.</param>
        internal void AttachReference(Object value)
        {
            var references = default(List<WeakReference>);
            AttachedReferences.TryGetValue(this, out references);
            references.Add(new WeakReference(value));
        }

        /// <summary>
        /// Waits for the given task before raising the load event.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        internal void DelayLoad(Task task)
        {
            if (!IsReady && task != null && !task.IsCompleted)
            {
                AttachReference(task);
            }
        }

        /// <summary>
        /// Sets the focus to the provided element.
        /// </summary>
        /// <param name="element">The element to focus on.</param>
        internal void SetFocus(IElement element)
        {
            _focus = element;
        }

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        internal async Task FinishLoadingAsync()
        {
            var tasks = GetAttachedReferences<Task>().ToArray();
            ReadyState = DocumentReadyState.Interactive;

            while (_loadingScripts.Count > 0)
            {
                await this.WaitForReadyAsync().ConfigureAwait(false);
                await _loadingScripts.Dequeue().RunAsync(CancellationToken.None).ConfigureAwait(false);
            }

            this.QueueTask(RaiseDomContentLoaded);

            await TaskEx.WhenAll(tasks).ConfigureAwait(false);

            this.QueueTask(RaiseLoadedEvent);

            if (IsInBrowsingContext)
            {
                this.QueueTask(ShowPage);
            }

            this.QueueTask(EmptyAppCache);

            if (IsToBePrinted)
            {
                await PrintAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Potentially prompts the user to unload the document.
        /// </summary>
        /// <returns>True if unload okay, otherwise false.</returns>
        internal async Task<Boolean> PromptToUnloadAsync()
        {
            var descendants = GetAttachedReferences<IBrowsingContext>();

            if (_view.HasEventListener(EventNames.BeforeUnload))
            {
                var unloadEvent = new Event(EventNames.BeforeUnload, bubbles: false, cancelable: true);
                var shouldCancel = _view.Fire(unloadEvent);
                _salvageable = false;

                if (shouldCancel)
                {
                    var data = new
                    {
                        Document = this,
                        IsCancelled = true,
                    };
                    await _context.FireAsync(EventNames.ConfirmUnload, data).ConfigureAwait(false);

                    if (data.IsCancelled)
                    {
                        return false;
                    }
                }
            }
             
            foreach (var descendant in descendants)
            {
                var active = descendant.Active as Document;

                if (active != null)
                {
                    var result = await active.PromptToUnloadAsync().ConfigureAwait(false);

                    if (!result)
                    {
                        return false;
                    }

                    _salvageable = _salvageable && active._salvageable;
                }
            }

            return true;
        }

        /// <summary>
        /// Unloads the document. For more details, see:
        /// http://www.w3.org/html/wg/drafts/html/CR/browsers.html#unload-a-document
        /// </summary>
        /// <param name="recycle">The recycle parameter.</param>
        internal void Unload(Boolean recycle)
        {
            var descendants = GetAttachedReferences<IBrowsingContext>();

            if (_shown)
            {
                _shown = false;
                this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageHide, false, false, _salvageable), _view);
            }

            if (_view.HasEventListener(EventNames.Unload))
            {
                if (!_firedUnload)
                {
                    _view.FireSimpleEvent(EventNames.Unload);
                    _firedUnload = true;
                }

                _salvageable = false;
            }

            CancelTasks();
            
            foreach (var descendant in descendants)
            {
                var active = descendant.Active as Document;

                if (active != null)
                {
                    active.Unload(false);
                    _salvageable = _salvageable && active._salvageable;
                }
            }

            if (!recycle && !_salvageable)
            {
                if (_context.Active == this)
                {
                    _context.Active = null;
                }
            }
        }

        #endregion

        #region Commands

        Boolean IDocument.ExecuteCommand(String commandId, Boolean showUserInterface, String value)
        {
            var command = Options.GetCommand(commandId);
            return command?.Execute(this, showUserInterface, value) ?? false;
        }

        Boolean IDocument.IsCommandEnabled(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command?.IsEnabled(this) ?? false;
        }

        Boolean IDocument.IsCommandIndeterminate(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command?.IsIndeterminate(this) ?? false;
        }

        Boolean IDocument.IsCommandExecuted(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command?.IsExecuted(this) ?? false;
        }

        Boolean IDocument.IsCommandSupported(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command?.IsSupported(this) ?? false;
        }

        String IDocument.GetCommandValue(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command?.GetValue(this);
        }

        #endregion

        #region Helpers

        private void Abort(Boolean fromUser = false)
        {
            if (fromUser && Object.ReferenceEquals(_context.Active, this))
            {
                this.QueueTask(() => _view.FireSimpleEvent(EventNames.Abort));
            }

            var childContexts = GetAttachedReferences<IBrowsingContext>();

            foreach (var childContext in childContexts)
            {
                var active = childContext.Active as Document;

                if (active != null)
                {
                    active.Abort(fromUser: false);
                    _salvageable = _salvageable && active._salvageable;
                }
            }

            var downloads = _loader.GetDownloads().Where(m => !m.IsCompleted);

            foreach (var download in downloads)
            {
                download.Cancel();
                _salvageable = false;
            }
        }

        private void CancelTasks()
        {
            foreach (var task in GetAttachedReferences<CancellationTokenSource>())
            {
                if (!task.IsCancellationRequested)
                {
                    task.Cancel();
                }
            }
        }

        private static Boolean IsCommand(IElement element)
        {
            return element is IHtmlMenuItemElement || element is IHtmlButtonElement || element is IHtmlAnchorElement;
        }

        private static Boolean IsLink(IElement element)
        {
            var isLinkElement = element is IHtmlAnchorElement || element is IHtmlAreaElement;
            return isLinkElement && element.Attributes.Any(m => m.Name.Is(AttributeNames.Href));
        }

        private static Boolean IsAnchor(IHtmlAnchorElement element)
        {
            return element.Attributes.Any(m => m.Name.Is(AttributeNames.Name));
        }

        private void RaiseDomContentLoaded()
        {
            this.FireSimpleEvent(EventNames.DomContentLoaded);
        }

        private void RaiseLoadedEvent()
        {
            ReadyState = DocumentReadyState.Complete;
            this.FireSimpleEvent(EventNames.Load);
        }

        private void EmptyAppCache()
        {
            //TODO
            //If the Document has any pending application cache download
            //process tasks, then queue each such task in the order they were
            //added to the list of pending application cache download process
            //tasks, and then empty the list of pending application cache
            //download process tasks. The task source for these tasks is the
            //networking task source.
        }

        private async Task PrintAsync()
        {
            var data = new { Document = this };
            this.FireSimpleEvent(EventNames.BeforePrint);
            await _context.FireAsync(EventNames.Print, data).ConfigureAwait(false);
            this.FireSimpleEvent(EventNames.AfterPrint);
        }

        private void ShowPage()
        {
            if (!_shown)
            {
                _shown = true;
                this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageShow, false, false, false), _view);
            }
        }

        private async void LocationChanged(Object sender, Location.LocationChangedEventArgs e)
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
                var request = DocumentRequest.Get(url, source: this, referer: DocumentUri);
                await _context.OpenAsync(request, CancellationToken.None);
            }
        }

        protected void Setup(CreateDocumentOptions options)
        {
            ContentType = options.ContentType.Content;
            StatusCode = options.Response.StatusCode;
            Referrer = options.Response.Headers.GetOrDefault(HeaderNames.Referer, String.Empty);
            DocumentUri = options.Response.Address.Href;
            Cookie = options.Response.Headers.GetOrDefault(HeaderNames.SetCookie, String.Empty);
            ImportAncestor = options.ImportAncestor;
            ReadyState = DocumentReadyState.Loading;
        }

        protected sealed override String LocateNamespace(String prefix)
        {
            return DocumentElement?.LocateNamespaceFor(prefix);
        }

        protected sealed override String LocatePrefix(String namespaceUri)
        {
            return DocumentElement?.LocatePrefixFor(namespaceUri);
        }

        protected void CloneDocument(Document document, Boolean deep)
        {
            CloneNode(document, deep);
            document._ready = _ready;
            document.Referrer = Referrer;
            document._location.Href = _location.Href;
            document._quirksMode = _quirksMode;
            document._sandbox = _sandbox;
            document._async = _async;
            document.ContentType = ContentType;
        }

        protected virtual String GetTitle()
        {
            return String.Empty;
        }

        protected abstract void SetTitle(String value);

        #endregion
    }
}

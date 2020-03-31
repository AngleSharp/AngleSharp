namespace AngleSharp.Dom
{
    using AngleSharp.Browser;
    using AngleSharp.Common;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using AngleSharp.Mathml.Dom;
    using AngleSharp.Svg.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node.
    /// </summary>
    public abstract class Document : Node, IDocument
    {
        #region Fields

        private readonly List<WeakReference> _attachedReferences;
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

        /// <inheritdoc />
        public event DomEventHandler ReadyStateChanged
        {
            add { AddEventListener(EventNames.ReadyStateChanged, value); }
            remove { RemoveEventListener(EventNames.ReadyStateChanged, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        /// <inheritdoc />
        public event DomEventHandler Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        #endregion

        #region ctor

        /// <inheritdoc />
        public Document(IBrowsingContext context, TextSource source)
            : base(null, "#document", NodeType.Document)
        {
            Referrer = String.Empty;
            ContentType = MimeTypeNames.ApplicationXml;
            _attachedReferences = new List<WeakReference>();
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
            _loader = context.GetService<IResourceLoader>();
            _loop = context.GetService<IEventLoop>();
            _mutations = new MutationHost(_loop);
            _statusCode = HttpStatusCode.OK;
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        public TextSource Source => _source;

        /// <inheritdoc />
        public abstract IEntityProvider Entities
        {
            get;
        }

        /// <inheritdoc />
        public IDocument ImportAncestor
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public IEventLoop Loop => _loop;

        /// <inheritdoc />
        public String DesignMode
        {
            get => _designMode ? Keywords.On : Keywords.Off;
            set => _designMode = value.Isi(Keywords.On);
        }

        /// <inheritdoc />
        public IHtmlAllCollection All => _all ?? (_all = new HtmlAllCollection(this));

        /// <inheritdoc />
        public IHtmlCollection<IHtmlAnchorElement> Anchors => _anchors ?? (_anchors = new HtmlCollection<IHtmlAnchorElement>(this, predicate: IsAnchor));

        /// <inheritdoc />
        public Int32 ChildElementCount => ChildNodes.OfType<Element>().Count();

        /// <inheritdoc />
        public IHtmlCollection<IElement> Children => _children ?? (_children = new HtmlCollection<IElement>(ChildNodes.OfType<Element>()));

        /// <inheritdoc />
        public IElement FirstElementChild
        {
            get
            {
                var children = ChildNodes;
                var n = children.Length;

                for (var i = 0; i < n; i++)
                {
                    if (children[i] is IElement child)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public IElement LastElementChild
        {
            get
            {
                var children = ChildNodes;

                for (var i = children.Length - 1; i >= 0; i--)
                {
                    if (children[i] is IElement child)
                    {
                        return child;
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public Boolean IsAsync => _async;

        /// <inheritdoc />
        public IHtmlScriptElement CurrentScript => _loadingScripts.Count > 0 ? _loadingScripts.Peek() : null;

        /// <inheritdoc />
        public IImplementation Implementation => _implementation ?? (_implementation = new DomImplementation(this));

        /// <inheritdoc />
        public String LastModified
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public IDocumentType Doctype => this.FindChild<DocumentType>();

        /// <inheritdoc />
        public String ContentType
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public DocumentReadyState ReadyState
        {
            get => _ready;
            protected set
            {
                _ready = value;
                this.FireSimpleEvent(EventNames.ReadyStateChanged);
            }
        }

        /// <inheritdoc />
        public IStyleSheetList StyleSheets => _styleSheets ?? (_styleSheets = this.CreateStyleSheets());

        /// <inheritdoc />
        public IStringList StyleSheetSets => _styleSheetSets ?? (_styleSheetSets = this.CreateStyleSheetSets());

        /// <inheritdoc />
        public String Referrer
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public ILocation Location => _location;

        /// <inheritdoc />
        public String DocumentUri
        {
            get => _location.Href;
            protected set
            {
                _location.Changed -= LocationChanged;
                _location.Href = value;
                _location.Changed += LocationChanged;
            }
        }

        /// <inheritdoc />
        public Url DocumentUrl => _location.Original;

        /// <inheritdoc />
        public IWindow DefaultView => _view;

        /// <inheritdoc />
        public String Direction
        {
            get => (DocumentElement as IHtmlElement ?? new HtmlHtmlElement(this)).Direction;
            set => (DocumentElement as IHtmlElement ?? new HtmlHtmlElement(this)).Direction = value;
        }

        /// <inheritdoc />
        public String CharacterSet => _source.CurrentEncoding.WebName;

        /// <inheritdoc />
        public abstract IElement DocumentElement
        {
            get;
        }

        /// <inheritdoc />
        public IElement ActiveElement => All.Where(m => m.IsFocused).FirstOrDefault();

        /// <inheritdoc />
        public String CompatMode => _quirksMode.GetCompatiblity();

        /// <inheritdoc />
        public String Url => _location.Href;

        /// <inheritdoc />
        public IHtmlCollection<IHtmlFormElement> Forms => new HtmlCollection<IHtmlFormElement>(this);

        /// <inheritdoc />
        public IHtmlCollection<IHtmlImageElement> Images => _images ?? (_images = new HtmlCollection<IHtmlImageElement>(this));

        /// <inheritdoc />
        public IHtmlCollection<IHtmlScriptElement> Scripts => _scripts ?? (_scripts = new HtmlCollection<IHtmlScriptElement>(this));

        /// <inheritdoc />
        public IHtmlCollection<IHtmlEmbedElement> Plugins => _plugins ?? (_plugins = new HtmlCollection<IHtmlEmbedElement>(this));

        /// <inheritdoc />
        public IHtmlCollection<IElement> Commands => _commands ?? (_commands = new HtmlCollection<IElement>(this, predicate: IsCommand));

        /// <inheritdoc />
        public IHtmlCollection<IElement> Links => _links ?? (_links = new HtmlCollection<IElement>(this, predicate: IsLink));

        /// <inheritdoc />
        public String Title
        {
            get => GetTitle();
            set => SetTitle(value);
        }

        /// <inheritdoc />
        public IHtmlHeadElement Head => DocumentElement.FindChild<IHtmlHeadElement>();

        /// <inheritdoc />
        public IHtmlElement Body
        {
            get
            {
                var root = DocumentElement;

                if (root != null)
                {
                    foreach (var child in root.ChildNodes)
                    {

                        if (child is HtmlBodyElement body)
                        {
                            return body;
                        }


                        if (child is HtmlFrameSetElement frameset)
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

        /// <inheritdoc />
        public IBrowsingContext Context => _context;

        /// <inheritdoc />
        public HttpStatusCode StatusCode
        {
            get => _statusCode;
            private set => _statusCode = value;
        }

        /// <inheritdoc />
        public String Cookie
        {
            get => _context.GetCookie(_location.Original);
            set => _context.SetCookie(_location.Original, value);
        }

        /// <inheritdoc />
        public String Domain
        {
            get => String.IsNullOrEmpty(DocumentUri) ? String.Empty : new Uri(DocumentUri).Host;
            set { if (_location == null) return; _location.Host = value; }
        }

        /// <inheritdoc />
        public String Origin => _location.Origin;

        /// <inheritdoc />
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

        /// <inheritdoc />
        public String LastStyleSheetSet
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public String PreferredStyleSheetSet => All.OfType<IHtmlLinkElement>().Where(m => m.IsPreferred()).Select(m => m.Title).FirstOrDefault();

        /// <inheritdoc />
        public Boolean IsReady => ReadyState == DocumentReadyState.Complete;

        /// <inheritdoc />
        public Boolean IsLoading => ReadyState == DocumentReadyState.Loading;

        #endregion

        #region Internal Properties

        internal MutationHost Mutations => _mutations;

        internal QuirksMode QuirksMode
        {
            get => _quirksMode;
            set => _quirksMode = value;
        }

        internal Sandboxes ActiveSandboxing
        {
            get => _sandbox;
            set => _sandbox = value;
        }

        internal void AddScript(HtmlScriptElement script)
        {
            _loadingScripts.Enqueue(script);
        }

        internal Boolean IsInBrowsingContext => _context.Active != null;

        internal Boolean IsToBePrinted => false;

        internal IElement FocusElement => _focus;

        #endregion

        #region Methods

        /// <summary>
        /// Clears the whole document without any notification.
        /// </summary>
        public void Clear() => ReplaceAll(null, true);

        /// <inheritdoc />
        public void Dispose()
        {
            //Important to fix #45
            Clear();
            _loop?.CancelAll();
            _loadingScripts.Clear();
            _source.Dispose();
            _view?.Dispose();
        }

        /// <inheritdoc />
        public void EnableStyleSheetsForSet(String name)
        {
            if (name != null)
            {
                StyleSheets.EnableStyleSheetSet(name);
            }
        }

        /// <inheritdoc />
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

                    Unload(recycle: true).Wait();
                    Abort();
                    RemoveEventListeners();

                    foreach (var element in this.Descendents<Element>())
                    {
                        element.RemoveEventListeners();
                    }

                    _loop?.CancelAll();
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

        /// <inheritdoc />
        public void Load(String url) => Location.Href = url;

        void IDocument.Close()
        {
            if (IsLoading)
            {
                FinishLoadingAsync().Wait();
            }
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void WriteLine(String content) => Write(content + Symbols.LineFeed);

        /// <inheritdoc />
        public IHtmlCollection<IElement> GetElementsByName(String name)
        {
            var result = new List<IElement>();
            ChildNodes.GetElementsByName(name, result);
            return new HtmlCollection<IElement>(result);
        }

        /// <inheritdoc />
        public INode Import(INode externalNode, Boolean deep = true)
        {
            if (externalNode.NodeType == NodeType.Document)
                throw new DomException(DomError.NotSupported);

            return externalNode.Clone(deep);
        }

        /// <inheritdoc />
        public INode Adopt(INode externalNode)
        {
            if (externalNode.NodeType == NodeType.Document)
                throw new DomException(DomError.NotSupported);

            this.AdoptNode(externalNode);
            return externalNode;
        }

        /// <inheritdoc />
        public Event CreateEvent(String type)
        {
            var factory = _context.GetFactory<IEventFactory>();
            var ev = factory.Create(type) ?? throw new DomException(DomError.NotSupported);
            return ev;
        }

        /// <inheritdoc />
        public INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null) => new NodeIterator(root, settings, filter);

        /// <inheritdoc />
        public ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null) => new TreeWalker(root, settings, filter);

        /// <inheritdoc />
        public IRange CreateRange()
        {
            var range = new Range(this);
            AttachReference(range);
            return range;
        }

        /// <inheritdoc />
        public void Prepend(params INode[] nodes) => this.PrependNodes(nodes);

        /// <inheritdoc />
        public void Append(params INode[] nodes) => this.AppendNodes(nodes);

        /// <inheritdoc />
        public IElement CreateElement(String localName)
        {
            if (localName.IsXmlName())
            {
                var factory = _context.GetFactory<IElementFactory<Document, HtmlElement>>();
                var element = factory.Create(this, localName);
                element.SetupElement();
                return element;
            }

            throw new DomException(DomError.InvalidCharacter);
        }

        /// <inheritdoc />
        public IElement CreateElement(String namespaceUri, String qualifiedName)
        {
            GetPrefixAndLocalName(qualifiedName, ref namespaceUri, out var prefix, out var localName);

            if (namespaceUri.Is(NamespaceNames.HtmlUri))
            {
                var factory = _context.GetFactory<IElementFactory<Document, HtmlElement>>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else if (namespaceUri.Is(NamespaceNames.SvgUri))
            {
                var factory = _context.GetFactory<IElementFactory<Document, SvgElement>>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else if (namespaceUri.Is(NamespaceNames.MathMlUri))
            {
                var factory = _context.GetFactory<IElementFactory<Document, MathElement>>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else
            {
                var element = new AnyElement(this, localName, prefix, namespaceUri);
                element.SetupElement();
                return element;
            }
        }

        /// <inheritdoc />
        public IComment CreateComment(String data) => new Comment(this, data);

        /// <inheritdoc />
        public IDocumentFragment CreateDocumentFragment() => new DocumentFragment(this);

        /// <inheritdoc />
        public IProcessingInstruction CreateProcessingInstruction(String target, String data)
        {
            if (!target.IsXmlName() || data.Contains("?>"))
                throw new DomException(DomError.InvalidCharacter);

            return new ProcessingInstruction(this, target) { Data = data };
        }

        /// <inheritdoc />
        public IText CreateTextNode(String data) => new TextNode(this, data);

        /// <inheritdoc />
        public IElement GetElementById(String elementId) => ChildNodes.GetElementById(elementId);

        /// <inheritdoc />
        public IElement QuerySelector(String selectors) => ChildNodes.QuerySelector(selectors, DocumentElement);

        /// <inheritdoc />
        public IHtmlCollection<IElement> QuerySelectorAll(String selectors) => ChildNodes.QuerySelectorAll(selectors, DocumentElement);

        /// <inheritdoc />
        public IHtmlCollection<IElement> GetElementsByClassName(String classNames) => ChildNodes.GetElementsByClassName(classNames);

        /// <inheritdoc />
        public IHtmlCollection<IElement> GetElementsByTagName(String tagName) => ChildNodes.GetElementsByTagName(tagName);

        /// <inheritdoc />
        public IHtmlCollection<IElement> GetElementsByTagName(String namespaceURI, String tagName) => ChildNodes.GetElementsByTagName(namespaceURI, tagName);

        /// <inheritdoc />
        public Boolean HasFocus() => Object.ReferenceEquals(_context.Active, this);

        /// <inheritdoc />
        public IAttr CreateAttribute(String localName)
        {
            if (!localName.IsXmlName())
                throw new DomException(DomError.InvalidCharacter);

            return new Attr(localName);
        }

        /// <inheritdoc />
        public IAttr CreateAttribute(String namespaceUri, String qualifiedName)
        {
            GetPrefixAndLocalName(qualifiedName, ref namespaceUri, out var prefix, out var localName);
            return new Attr(prefix, localName, String.Empty, namespaceUri);
        }

        /// <summary>
        /// Sets the document up with the given parameters.
        /// </summary>
        /// <param name="response">The received response.</param>
        /// <param name="contentType">The content-type.</param>
        /// <param name="importAncestor">The ancestor, if any.</param>
        public void Setup(IResponse response, MimeType contentType, IDocument importAncestor)
        {
            ContentType = contentType.Content;
            StatusCode = response.StatusCode;
            Referrer = response.Headers.GetOrDefault(HeaderNames.Referer, String.Empty);
            DocumentUri = response.Address.Href;
            Cookie = response.Headers.GetOrDefault(HeaderNames.SetCookie, String.Empty);
            ImportAncestor = importAncestor;
            ReadyState = DocumentReadyState.Loading;
        }

        /// <summary>
        /// Creates a new element in the current namespace from the infos.
        /// </summary>
        /// <param name="name">The name of the new element.</param>
        /// <param name="prefix">The optional prefix to use.</param>
        /// <param name="flags">The optional flags, if any.</param>
        /// <returns>The created element.</returns>
        public abstract Element CreateElementFrom(String name, String prefix, NodeFlags flags = NodeFlags.None);

        /// <summary>
        /// Waits for the given task before raising the load event.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        public void DelayLoad(Task task)
        {
            if (!IsReady && task != null && !task.IsCompleted)
            {
                AttachReference(task);
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the specified attached references.
        /// </summary>
        /// <typeparam name="T">The type of values to get.</typeparam>
        /// <returns>Gets the enumeration over all values.</returns>
        internal IEnumerable<T> GetAttachedReferences<T>()
            where T : class => _attachedReferences.Select(entry => entry.IsAlive ? entry.Target as T : null).Where(m => m != null);

        /// <summary>
        /// Attaches another reference to this document.
        /// </summary>
        /// <param name="value">The value to attach.</param>
        internal void AttachReference(Object value) => _attachedReferences.Add(new WeakReference(value));

        /// <summary>
        /// Sets the focus to the provided element.
        /// </summary>
        /// <param name="element">The element to focus on.</param>
        internal void SetFocus(IElement element) => _focus = element;

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

            this.FireSimpleEvent(EventNames.DomContentLoaded);
            _view.FireSimpleEvent(EventNames.DomContentLoaded);

            await Task.WhenAll(tasks).ConfigureAwait(false);

            ReadyState = DocumentReadyState.Complete;

            Body?.FireSimpleEvent(EventNames.Load);
            this.FireSimpleEvent(EventNames.Load);
            _view.FireSimpleEvent(EventNames.Load);

            if (IsInBrowsingContext && !_shown)
            {
                _shown = true;
                this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageShow, false, false, false), _view);
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
                var shouldCancel = await this.QueueTaskAsync(_ => _view.Fire(unloadEvent)).ConfigureAwait(false);

                _salvageable = false;

                if (shouldCancel)
                {
                    var data = new
                    {
                        Document = this,
                        IsCancelled = true,
                    };
                    await _context.InteractAsync(EventNames.ConfirmUnload, data).ConfigureAwait(false);

                    if (data.IsCancelled)
                    {
                        return false;
                    }
                }
            }

            foreach (var descendant in descendants)
            {
                if (descendant.Active is Document active)
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
        internal async Task Unload(Boolean recycle)
        {
            var descendants = GetAttachedReferences<IBrowsingContext>();

            if (_shown)
            {
                _shown = false;
                await this.QueueTaskAsync(_ =>
                {
                    this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageHide, false, false, _salvageable), _view);
                }).ConfigureAwait(false);
            }

            if (_view.HasEventListener(EventNames.Unload))
            {
                if (!_firedUnload)
                {
                    await this.QueueTaskAsync(_ => _view.FireSimpleEvent(EventNames.Unload)).ConfigureAwait(false);
                    _firedUnload = true;
                }

                _salvageable = false;
            }

            CancelTasks();

            foreach (var descendant in descendants)
            {
                if (descendant.Active is Document active)
                {
                    await active.Unload(false).ConfigureAwait(false);
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
            var command = _context.GetCommand(commandId);
            return command?.Execute(this, showUserInterface, value) ?? false;
        }

        Boolean IDocument.IsCommandEnabled(String commandId)
        {
            var command = _context.GetCommand(commandId);
            return command?.IsEnabled(this) ?? false;
        }

        Boolean IDocument.IsCommandIndeterminate(String commandId)
        {
            var command = _context.GetCommand(commandId);
            return command?.IsIndeterminate(this) ?? false;
        }

        Boolean IDocument.IsCommandExecuted(String commandId)
        {
            var command = _context.GetCommand(commandId);
            return command?.IsExecuted(this) ?? false;
        }

        Boolean IDocument.IsCommandSupported(String commandId)
        {
            var command = _context.GetCommand(commandId);
            return command?.IsSupported(this) ?? false;
        }

        String IDocument.GetCommandValue(String commandId)
        {
            var command = _context.GetCommand(commandId);
            return command?.GetValue(this);
        }

        #endregion

        #region Helpers

        private void Abort(Boolean fromUser = false)
        {
            if (fromUser && Object.ReferenceEquals(_context.Active, this))
            {
                this.QueueTaskAsync(_ => _view.FireSimpleEvent(EventNames.Abort));
            }

            var childContexts = GetAttachedReferences<IBrowsingContext>();

            foreach (var childContext in childContexts)
            {
                if (childContext.Active is Document active)
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

        private static Boolean IsCommand(IElement element) => element is IHtmlMenuItemElement || element is IHtmlButtonElement || element is IHtmlAnchorElement;

        private static Boolean IsLink(IElement element)
        {
            var isLinkElement = element is IHtmlAnchorElement || element is IHtmlAreaElement;
            return isLinkElement && element.Attributes.Any(m => m.Name.Is(AttributeNames.Href));
        }

        private static Boolean IsAnchor(IHtmlAnchorElement element) => element.Attributes.Any(m => m.Name.Is(AttributeNames.Name));

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
            await this.QueueTaskAsync(_ => this.FireSimpleEvent(EventNames.BeforePrint)).ConfigureAwait(false);
            await _context.InteractAsync(EventNames.Print, new { Document = this }).ConfigureAwait(false);
            await this.QueueTaskAsync(_ => this.FireSimpleEvent(EventNames.AfterPrint)).ConfigureAwait(false);
        }

        private async void LocationChanged(Object sender, Location.ChangedEventArgs e)
        {
            if (e.IsHashChanged)
            {
                var ev = new HashChangedEvent();
                ev.Init(EventNames.HashChange, false, false, e.PreviousLocation, e.CurrentLocation);
                ev.IsTrusted = true;
                this.QueueTask(() => ev.Dispatch(_view));
            }
            else if (!e.IsReloaded)
            {
                var url = new Url(e.CurrentLocation);
                var request = DocumentRequest.Get(url, source: this, referer: DocumentUri);
                await _context.OpenAsync(request, CancellationToken.None);
            }
            else
            {
                var url = _location.Original;
                var request = DocumentRequest.Get(url, source: this, referer: Referrer);
                await _context.OpenAsync(request, CancellationToken.None);
            }
        }

        /// <inheritdoc />
        protected sealed override String LocateNamespace(String prefix)
        {
            return DocumentElement?.LocateNamespaceFor(prefix);
        }

        /// <inheritdoc />
        protected sealed override String LocatePrefix(String namespaceUri)
        {
            return DocumentElement?.LocatePrefixFor(namespaceUri);
        }

        /// <inheritdoc />
        protected void CloneDocument(Document document, Boolean deep)
        {
            CloneNode(document, document, deep);
            document._ready = _ready;
            document.Referrer = Referrer;
            document._location.Href = _location.Href;
            document._quirksMode = _quirksMode;
            document._sandbox = _sandbox;
            document._async = _async;
            document.ContentType = ContentType;
        }

        /// <inheritdoc />
        protected virtual String GetTitle() => String.Empty;

        /// <inheritdoc />
        protected abstract void SetTitle(String value);

        #endregion
    }
}

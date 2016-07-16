namespace AngleSharp.Dom
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a document node.
    /// </summary>
    [DebuggerStepThrough]
    abstract class Document : Node, IDocument
    {
        #region Fields

        readonly Queue<HtmlScriptElement> _loadingScripts;
        readonly List<WeakReference<Range>> _ranges;
        readonly MutationHost _mutations;
        readonly IBrowsingContext _context;
        readonly IEventLoop _loop;
        readonly Window _view;
        readonly IResourceLoader _loader;
        readonly Location _location;
        readonly TextSource _source;
        readonly List<Task> _subtasks;

        QuirksMode _quirksMode;
        Sandboxes _sandbox;
        Boolean _async;
        Boolean _designMode;
        Boolean _shown;
        Boolean _salvageable;
        Boolean _firedUnload;
        DocumentReadyState _ready;
        IElement _focus;
        HtmlAllCollection _all;
        HtmlCollection<IHtmlAnchorElement> _anchors;
        HtmlElementCollection _children;
        DomImplementation _implementation;
        IStringList _styleSheetSets;
        HtmlCollection<IHtmlImageElement> _images;
        HtmlCollection<IHtmlScriptElement> _scripts;
        HtmlCollection<IHtmlEmbedElement> _plugins;
        HtmlElementCollection _commands;
        HtmlElementCollection _links;
        IStyleSheetList _styleSheets;
        HttpStatusCode _statusCode;

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
            _async = true;
            _designMode = false;
            _firedUnload = false;
            _salvageable = true;
            _shown = false;
            _context = context;
            _source = source;
            Referrer = String.Empty;
            ContentType = MimeTypeNames.ApplicationXml;
            _ready = DocumentReadyState.Loading;
            _sandbox = Sandboxes.None;
            _quirksMode = QuirksMode.Off;
            _loadingScripts = new Queue<HtmlScriptElement>();
            _location = new Location("about:blank");
            _ranges = new List<WeakReference<Range>>();
            _location.Changed += LocationChanged;
            _view = new Window(this);
            _loader = context.CreateService<IResourceLoader>();
            _loop = context.CreateService<IEventLoop>();
            _mutations = new MutationHost(_loop);
            _subtasks = new List<Task>();
            _statusCode = HttpStatusCode.OK;
        }

        #endregion

        #region Properties

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
            get { return _children ?? (_children = new HtmlElementCollection(ChildNodes.OfType<Element>())); }
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
            get { return _commands ?? (_commands = new HtmlElementCollection(this, predicate: IsCommand)); }
        }

        public IHtmlCollection<IElement> Links
        {
            get { return _links ?? (_links = new HtmlElementCollection(this, predicate: IsLink)); }
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
                {
                    throw new DomException(DomError.HierarchyRequest);
                }

                var body = Body;

                if (body != value)
                {
                    if (body == null)
                    {
                        var root = DocumentElement;

                        if (root == null)
                        {
                            throw new DomException(DomError.HierarchyRequest);
                        }
                        
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

        internal IEnumerable<Range> Ranges
        {
            get 
            { 
                return _ranges.Select(entry => 
                {
                    var range = default(Range);
                    entry.TryGetTarget(out range);
                    return range;
                }).Where(range => range != null); 
            }
        }

        internal MutationHost Mutations
        {
            get { return _mutations; }
        }

        internal TextSource Source
        {
            get { return _source; }
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
            _loop.Shutdown();
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
            {
                throw new DomException(DomError.InvalidState);
            }

            if (!IsInBrowsingContext || Object.ReferenceEquals(_context.Active, this))
            {
                var shallReplace = replace.Isi(Keywords.Replace);

                if (_loadingScripts.Count == 0)
                {
                    if (shallReplace)
                    {
                        type = MimeTypeNames.Html;
                    }

                    var index = type.IndexOf(Symbols.Semicolon);

                    if (index >= 0)
                    {
                        type = type.Substring(0, index);
                    }

                    type = type.StripLeadingTrailingSpaces();
                    //TODO further steps needed.
                    //see https://html.spec.whatwg.org/multipage/webappapis.html#dom-document-open
                    ContentType = type;
                    ReplaceAll(null, false);
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
            return new HtmlElementCollection(result);
        }

        public INode Import(INode externalNode, Boolean deep = true)
        {
            if (externalNode.NodeType == NodeType.Document)
            {
                throw new DomException(DomError.NotSupported);
            }

            return externalNode.Clone(deep);
        }

        public INode Adopt(INode externalNode)
        {
            if (externalNode.NodeType == NodeType.Document)
            {
                throw new DomException(DomError.NotSupported);
            }

            this.AdoptNode(externalNode);
            return externalNode;
        }

        public Event CreateEvent(String type)
        {
            var factory = Options.GetFactory<IEventFactory>();
            var ev = factory.Create(type);

            if (ev == null)
            {
                throw new DomException(DomError.NotSupported);
            }

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
            _ranges.Add(new WeakReference<Range>(range));
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
                var factory = Options.GetFactory<IHtmlElementFactory>();
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
                var factory = Options.GetFactory<IHtmlElementFactory>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else if (namespaceUri.Is(NamespaceNames.SvgUri))
            {
                var factory = Options.GetFactory<ISvgElementFactory>();
                var element = factory.Create(this, localName, prefix);
                element.SetupElement();
                return element;
            }
            else if (namespaceUri.Is(NamespaceNames.MathMlUri))
            {
                var factory = Options.GetFactory<IMathElementFactory>();
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
            {
                throw new DomException(DomError.InvalidCharacter);
            }

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
            {
                throw new DomException(DomError.InvalidCharacter);
            }

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
        /// Waits for the given task before raising the load event.
        /// </summary>
        /// <param name="task">The task to wait for.</param>
        internal void DelayLoad(Task task)
        {
            if (!IsReady && task != null && !task.IsCompleted)
            {
                _subtasks.Add(task);
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
            ReadyState = DocumentReadyState.Interactive;

            while (_loadingScripts.Count > 0)
            {
                await this.WaitForReadyAsync().ConfigureAwait(false);
                await _loadingScripts.Dequeue().RunAsync(CancellationToken.None).ConfigureAwait(false);
            }

            this.QueueTask(RaiseDomContentLoaded);

            await TaskEx.WhenAll(_subtasks.ToArray()).ConfigureAwait(false);

            this.QueueTask(RaiseLoadedEvent);

            if (IsInBrowsingContext)
            {
                this.QueueTask(ShowPage);
            }

            this.QueueTask(EmptyAppCache);

            if (IsToBePrinted)
            {
                Print();
            }
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
            if (_shown)
            {
                _shown = false;
                this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageHide, false, false, _salvageable), _view);
            }

            if (!_firedUnload)
            {
                _view.FireSimpleEvent(EventNames.Unload);
            }

            this.ReleaseStorageMutex();

            if (_view.HasEventListener(EventNames.Unload))
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
            return command != null ? command.Execute(this, showUserInterface, value) : false;
        }

        Boolean IDocument.IsCommandEnabled(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command != null ? command.IsEnabled(this) : false;
        }

        Boolean IDocument.IsCommandIndeterminate(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command != null ? command.IsIndeterminate(this) : false;
        }

        Boolean IDocument.IsCommandExecuted(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command != null ? command.IsExecuted(this) : false;
        }

        Boolean IDocument.IsCommandSupported(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command != null ? command.IsSupported(this) : false;
        }

        String IDocument.GetCommandValue(String commandId)
        {
            var command = Options.GetCommand(commandId);
            return command != null ? command.GetValue(this) : null;
        }

        #endregion

        #region Helpers

        static Boolean IsCommand(IElement element)
        {
            return element is IHtmlMenuItemElement || element is IHtmlButtonElement || element is IHtmlAnchorElement;
        }

        static Boolean IsLink(IElement element)
        {
            var isLinkElement = element is IHtmlAnchorElement || element is IHtmlAreaElement;
            return isLinkElement && element.Attributes.Any(m => m.Name.Is(AttributeNames.Href));
        }

        static Boolean IsAnchor(IHtmlAnchorElement element)
        {
            return element.Attributes.Any(m => m.Name.Is(AttributeNames.Name));
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
            if (!_shown)
            {
                _shown = true;
                this.Fire<PageTransitionEvent>(ev => ev.Init(EventNames.PageShow, false, false, false), _view);
            }
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
            return DocumentElement.LocateNamespace(prefix);
        }

        protected sealed override String LocatePrefix(String namespaceUri)
        {
            return DocumentElement.LocatePrefix(namespaceUri);
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

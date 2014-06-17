namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Parser;
    using AngleSharp.Parser.Html;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an HTML document.
    /// </summary>
    public sealed class HTMLDocument : Document, IHtmlDocument
    {
        #region Fields

        Cookie _cookie;
        Task _queue;
        HTMLCollection _all;
        HTMLCollection<HTMLFormElement> _forms;
        HTMLCollection<HTMLScriptElement> _scripts;
        HTMLCollection<HTMLImageElement> _images;
        HTMLCollection<HTMLAnchorElement> _anchors;
        HTMLCollection _embeds;
        HTMLCollection _links;

        #endregion

        #region Events

        //Internal for now until connected properly.

        //[DOM("onabort")]
        //internal event EventHandler OnAbort;
        //[DOM("onblur")]
        //internal event EventHandler OnBlur;
        //[DOM("oncanplay")]
        //internal event EventHandler OnCanPlay;
        //[DOM("oncanplaythrough")]
        //internal event EventHandler OnCanPlayThrough;
        //[DOM("onchange")]
        //internal event EventHandler OnChange;
        //[DOM("onclick")]
        //internal event EventHandler OnClick;
        //[DOM("oncontextmenu")]
        //internal event EventHandler OnContextMenu;
        //[DOM("oncopy")]
        //internal event EventHandler OnCopy;
        //[DOM("oncuechange")]
        //internal event EventHandler OnCueChange;
        //[DOM("oncut")]
        //internal event EventHandler OnCut;
        //[DOM("ondblclick")]
        //internal event EventHandler OnDblClick;
        //[DOM("onaondragbort")]
        //internal event EventHandler OnDrag;
        //[DOM("ondragend")]
        //internal event EventHandler OnDragEnd;
        //[DOM("ondragenter")]
        //internal event EventHandler OnDragEnter;
        //[DOM("ondragleave")]
        //internal event EventHandler OnDragLeave;
        //[DOM("ondragover")]
        //internal event EventHandler OnDragOver;
        //[DOM("ondragstart")]
        //internal event EventHandler OnDragStart;
        //[DOM("ondrop")]
        //internal event EventHandler OnDrop;
        //[DOM("ondurationchange")]
        //internal event EventHandler OnDurationChange;
        //[DOM("onemptied")]
        //internal event EventHandler OnEmptied;
        //[DOM("onended")]
        //internal event EventHandler OnEnded;
        //[DOM("onerror")]
        //internal event EventHandler OnError;
        //[DOM("onfocus")]
        //internal event EventHandler OnFocus;
        //[DOM("onfocusin")]
        //internal event EventHandler OnFocusIn;
        //[DOM("onfocusout")]
        //internal event EventHandler OnFocusOut;
        //[DOM("onfullscreenchange")]
        //internal event EventHandler OnFullScreenChange;
        //[DOM("onfullscreenerror")]
        //internal event EventHandler OnFullScreenError;
        //[DOM("oninput")]
        //internal event EventHandler OnInput;
        //[DOM("oninvalid")]
        //internal event EventHandler OnInvalid;
        //[DOM("onkeydown")]
        //internal event EventHandler OnKeyDown;
        //[DOM("onkeypress")]
        //internal event EventHandler OnKeyPress;
        //[DOM("onkeyup")]
        //internal event EventHandler OnKeyUp;
        //[DOM("onload")]
        //internal event EventHandler OnLoad;
        //[DOM("onloadeddata")]
        //internal event EventHandler OnLoadedData;
        //[DOM("onloadedmetadata")]
        //internal event EventHandler OnLoadedMetaData;
        //[DOM("onloadstart")]
        //internal event EventHandler OnLoadStart;
        //[DOM("onmousedown")]
        //internal event EventHandler OnMouseDown;
        //[DOM("onmousemove")]
        //internal event EventHandler OnMouseMove;
        //[DOM("onmouseout")]
        //internal event EventHandler OnMouseOut;
        //[DOM("onmouseover")]
        //internal event EventHandler OnMouseOver;
        //[DOM("onmouseup")]
        //internal event EventHandler OnMouseUp;
        //[DOM("onmousewheel")]
        //internal event EventHandler OnMouseWheel;
        //[DOM("onpaste")]
        //internal event EventHandler OnPaste;
        //[DOM("onpause")]
        //internal event EventHandler OnPause;
        //[DOM("onplay")]
        //internal event EventHandler OnPlay;
        //[DOM("onplaying")]
        //internal event EventHandler OnPlaying;
        //[DOM("onprogress")]
        //internal event EventHandler OnProgress;
        //[DOM("onratechange")]
        //internal event EventHandler OnRateChange;
        //[DOM("onreset")]
        //internal event EventHandler OnReset;
        //[DOM("onscroll")]
        //internal event EventHandler OnScroll;
        //[DOM("onseeked")]
        //internal event EventHandler OnSeeked;
        //[DOM("onseeking")]
        //internal event EventHandler OnSeeking;
        //[DOM("onselect")]
        //internal event EventHandler OnSelect;
        //[DOM("onstalled")]
        //internal event EventHandler OnStalled;
        //[DOM("onsubmit")]
        //internal event EventHandler OnSubmit;
        //[DOM("onsuspend")]
        //internal event EventHandler OnSuspend;
        //[DOM("ontimeout")]
        //internal event EventHandler OnTimeOut;
        //[DOM("ontimeupdate")]
        //internal event EventHandler OnTimeUpdate;
        //[DOM("ontouchcancel")]
        //internal event EventHandler OnTouchCancel;
        //[DOM("ontouchend")]
        //internal event EventHandler OnTouchEnd;
        //[DOM("ontouchmove")]
        //internal event EventHandler OnTouchMove;
        //[DOM("ontouchstart")]
        //internal event EventHandler OnTouchStart;
        //[DOM("onvolumechange")]
        //internal event EventHandler OnVolumeChange;
        //[DOM("onwaiting")]
        //internal event EventHandler OnWaiting;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new Html document.
        /// </summary>
        internal HTMLDocument()
        {
            _contentType = MimeTypes.Xml;
            _all = new HTMLCollection(this);
            _queue = new Task(() => { });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all elements in the document.
        /// </summary>
        [DomName("all")]
        public HTMLCollection All
        {
            get { return _all; }
        }

        /// <summary>
        /// Gets a list of all of the anchors in the document.
        /// </summary>
        [DomName("anchors")]
        public HTMLCollection<HTMLAnchorElement> Anchors
        {
            get { return _anchors ?? (_anchors = new HTMLCollection<HTMLAnchorElement>(this, predicate: element => element.Attributes.Any(m => m.Name == AttributeNames.Name))); }
        }

        /// <summary>
        /// Gets the forms in the document.
        /// </summary>
        [DomName("forms")]
        public HTMLCollection<HTMLFormElement> Forms
        {
            get { return _forms ?? (_forms = new HTMLCollection<HTMLFormElement>(this)); }
        }

        /// <summary>
        /// Gets the images in the document.
        /// </summary>
        [DomName("images")]
        public HTMLCollection<HTMLImageElement> Images
        {
            get { return _images ?? (_images = new HTMLCollection<HTMLImageElement>(this)); }
        }

        /// <summary>
        /// Gets the scripts in the document.
        /// </summary>
        [DomName("scripts")]
        public HTMLCollection<HTMLScriptElement> Scripts
        {
            get { return _scripts ?? (_scripts = new HTMLCollection<HTMLScriptElement>(this)); }
        }

        /// <summary>
        /// Gets a list of the embedded OBJECTS within the current document.
        /// </summary>
        [DomName("embeds")]
        public HTMLCollection Embeds
        {
            get { return _embeds ?? (_embeds = new HTMLCollection(this, predicate: element => element is HTMLEmbedElement || element is HTMLObjectElement || element is HTMLAppletElement)); }
        }

        /// <summary>
        /// Gets a collection of all AREA elements and anchor elements in a document with a value for the href attribute.
        /// </summary>
        [DomName("links")]
        public HTMLCollection Links
        {
            get { return _links ?? (_links = new HTMLCollection(this, predicate: element => (element is HTMLAnchorElement || element is HTMLAreaElement) && element.Attributes.Any(m => m.Name == AttributeNames.Href))); }
        }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        [DomName("title")]
        public String Title
        {
            get
            {
                var _title = FindChild<HTMLTitleElement>(Head);

                if (_title != null)
                    return _title.Text;

                return String.Empty;
            }
            set
            {
                var _title = FindChild<HTMLTitleElement>(Head);

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
        [DomName("head")]
        public HTMLHeadElement Head
        {
            get { return FindChild<HTMLHeadElement>(DocumentElement as Element); }//TODO Remove cast ASAP
        }

        /// <summary>
        /// Gets the body element.
        /// </summary>
        [DomName("body")]
        public HTMLBodyElement Body
        {
            get { return FindChild<HTMLBodyElement>(DocumentElement as Element); }//TODO Remove cast ASAP
        }

        /// <summary>
        /// Gets or sets the document cookie.
        /// </summary>
        [DomName("cookie")]
        public Cookie Cookie
        { 
            get { return _cookie; }
            set { _cookie = value; }
        }

        /// <summary>
        /// Gets the domain portion of the origin of the current document.
        /// </summary>
        [DomName("domain")]
        public String Domain
        {
            get { return String.IsNullOrEmpty(DocumentUri) ? String.Empty : new Uri(DocumentUri).Host; }
        }

        #endregion

        #region Static Helpers

        /// <summary>
        /// Loads a HTML document from the given URL.
        /// </summary>
        /// <param name="url">The URL that hosts the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The document with the parsed content.</returns>
        public static HTMLDocument LoadFromUrl(String url, IConfiguration configuration = null)
        {
            var doc = new HTMLDocument { Options = configuration ?? Configuration.Default };
            doc.Load(url);
            return doc;
        }

        /// <summary>
        /// Loads a HTML document from the given URL.
        /// </summary>
        /// <param name="source">The source code with the HTML content.</param>
        /// <param name="configuration">[Optional] Custom options to use for the document generation.</param>
        /// <returns>The document with the parsed content.</returns>
        public static HTMLDocument LoadFromSource(String source, IConfiguration configuration = null)
        {
            return DocumentBuilder.Html(source, configuration);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the document content from the given URL.
        /// </summary>
        /// <param name="url">The URL that hosts the HTML content.</param>
        [DomName("load")]
        public void Load(String url)
        {
            Uri uri;
            _location.Href = url;
            Cookie = new Cookie();
            
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
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override INode Clone(Boolean deep = true)
        {
            var node = new HTMLDocument();
            CopyProperties(this, node, deep);
            CopyDocumentProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Adopts a node from an external document. The node and its subtree is removed from
        /// the document it's in (if any), and its OwnerDocument is changed to the current document. 
        /// </summary>
        /// <param name="externalNode">The node from another document to be adopted.</param>
        /// <returns>The adopted node that can be used in the current document. </returns>
        [DomName("adoptNode")]
        public Node AdoptNode(Node externalNode)
        {
            if (externalNode.Owner != null)
            {
                if (externalNode.Parent != null)
                    externalNode.Parent.RemoveChild(externalNode);
            }

            externalNode.Owner = this;
            return externalNode;
        }

        /// <summary>
        /// Opens a document stream for writing.
        /// </summary>
        [DomName("open")]
        public void Open()
        {
            //TODO
        }

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        [DomName("close")]
        public void Close()
        {
            //TODO
        }

        /// <summary>
        /// Writes text to a document.
        /// </summary>
        /// <param name="content">The text to be written on the document.</param>
        [DomName("write")]
        public void Write(String content)
        {
            //TODO
        }

        /// <summary>
        /// Writes a line of text to a document.
        /// </summary>
        /// <param name="content">The text to be written on the document.</param>
        [DomName("writeln")]
        public void WriteLn(String content)
        {
            Write(content + Specification.LineFeed);
        }

        /// <summary>
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element object.</returns>
        public override IElement CreateElement(String tagName)
        {
            return HTMLFactory.Create(tagName, this);
        }

        /// <summary>
        /// Returns a list of elements with a given name in the HTML document.
        /// </summary>
        /// <param name="name">The value of the name attribute of the element.</param>
        /// <returns>A collection of HTML elements.</returns>
        public IHtmlCollection GetElementsByName(String name)
        {
            var result = new List<Element>();
            GetElementsByName(_children, name, result);
            return new HTMLCollection(result);
        }

        #endregion

        #region Internals

        internal Int32 ScriptsWaiting 
        { 
            get { return 0; } 
        }

        internal Int32 ScriptsAsSoonAsPossible 
        { 
            get { return 0; } 
        }

        internal void RunNextScript()
        {
            WaitForReady();
            //TODO Run first script that should be executed when the document is finished parsing
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

        /// <summary>
        /// Reloads the document witht he given location.
        /// </summary>
        /// <param name="url">The value for reloading.</param>
        protected override void ReLoad(Location url)
        {
            Load(url.Href);
        }

        /// <summary>
        /// Gets a list of HTML elements given by their name attribute.
        /// </summary>
        /// <param name="children">The list to investigate.</param>
        /// <param name="name">The name attribute's value.</param>
        /// <param name="result">The result collection.</param>
        static void GetElementsByName(NodeList children, String name, List<Element> result)
        {
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i] is HTMLElement)
                {
                    var element = (HTMLElement)children[i];

                    if (element.GetAttribute(AttributeNames.Name) == name)
                        result.Add(element);

                    GetElementsByName(element.ChildNodes, name, result);
                }
            }
        }

        #endregion
    }
}

namespace AngleSharp.Html.Dom
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using System;

    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    abstract class HtmlFrameElementBase : HtmlFrameOwnerElement
    {
        #region Fields

        private IBrowsingContext _context;
        private FrameRequestProcessor _request;

        #endregion

        #region ctor

        public HtmlFrameElementBase(Document owner, String name, String prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags | NodeFlags.Special)
        {
            _request = new FrameRequestProcessor(owner.Context, this);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload
        {
            get { return _request?.Download; }
        }

        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
        }

        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String Scrolling
        {
            get { return this.GetOwnAttribute(AttributeNames.Scrolling); }
            set { this.SetOwnAttribute(AttributeNames.Scrolling, value); }
        }

        public IDocument ContentDocument
        {
            get { return _request?.Document; }
        }

        public String LongDesc
        {
            get { return this.GetOwnAttribute(AttributeNames.LongDesc); }
            set { this.SetOwnAttribute(AttributeNames.LongDesc, value); }
        }

        public String FrameBorder
        {
            get { return this.GetOwnAttribute(AttributeNames.FrameBorder); }
            set { this.SetOwnAttribute(AttributeNames.FrameBorder, value); }
        }

        public IBrowsingContext NestedContext
        {
            get { return _context ?? (_context = NewChildContext()); }
        }

        #endregion

        #region Internal Methods

        internal virtual String GetContentHtml()
        {
            return null;
        }

        internal override void SetupElement()
        {
            base.SetupElement();

            if (this.GetOwnAttribute(AttributeNames.Src) != null)
            {
                UpdateSource();
            }
        }

        internal void UpdateSource()
        {
            var content = GetContentHtml();
            var source = Source;

            if ((source != null && source != Owner.DocumentUri) || content != null)
            {
                var url = this.HyperReference(source);
                this.Process(_request, url);
            }
        }

        #endregion

        #region Helpers

        private IBrowsingContext NewChildContext()
        {
            //TODO
            var childContext = Context.CreateChild(null, Sandboxes.None);
            Owner.AttachReference(childContext);
            return childContext;
        }

        #endregion
    }
}

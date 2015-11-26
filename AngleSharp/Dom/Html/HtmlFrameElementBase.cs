namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    abstract class HtmlFrameElementBase : HtmlFrameOwnerElement
    {
        #region Fields

        IBrowsingContext _context;

        #endregion

        #region ctor

        public HtmlFrameElementBase(Document owner, String name, String prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags | NodeFlags.Special)
        {
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the frame.
        /// </summary>
        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the frame source.
        /// </summary>
        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets whether or not the frame should have scrollbars.
        /// </summary>
        public String Scrolling
        {
            get { return this.GetOwnAttribute(AttributeNames.Scrolling); }
            set { this.SetOwnAttribute(AttributeNames.Scrolling, value); }
        }

        /// <summary>
        /// Gets the document this frame contains, if there is any and it is
        /// available, or null otherwise.
        /// </summary>
        public IDocument ContentDocument
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the URL designating a long description of this image
        /// or frame.
        /// </summary>
        public String LongDesc
        {
            get { return this.GetOwnAttribute(AttributeNames.LongDesc); }
            set { this.SetOwnAttribute(AttributeNames.LongDesc, value); }
        }

        /// <summary>
        /// Gets or sets the request frame borders.
        /// </summary>
        public String FrameBorder
        {
            get { return this.GetOwnAttribute(AttributeNames.FrameBorder); }
            set { this.SetOwnAttribute(AttributeNames.FrameBorder, value); }
        }

        /// <summary>
        /// Gets the nested context responsible for the content.
        /// </summary>
        protected IBrowsingContext NestedContext
        {
            get { return _context ?? (_context = Owner.NewChildContext(Sandboxes.None)); }
        }

        #endregion

        #region Methods

        protected virtual String GetContentHtml()
        {
            return null;
        }

        protected void UpdateSource(String _)
        {
            this.CancelTasks();
            var content = GetContentHtml();
            var src = Source;

            if (content != null)
            {
                this.CreateTask(cancel => NestedContext.OpenAsync(m => m.Content(content).Address(Owner.DocumentUri), cancel))
                    .ContinueWith(Finished);
            }
            else if (!String.IsNullOrEmpty(src) && !src.Is(BaseUri))
            {
                var url = this.HyperReference(src);
                var request = DocumentRequest.Get(url, source: this, referer: Owner.DocumentUri);
                this.CreateTask(cancel => NestedContext.OpenAsync(request, cancel))
                    .ContinueWith(Finished);
            }
        }

        protected void Finished(Task<IDocument> task)
        {
            if (!task.IsFaulted)
                ContentDocument = task.Result;

            this.FireLoadOrErrorEvent(task);
        }

        #endregion
    }
}

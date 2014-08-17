namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    sealed class HTMLLinkElement : HTMLElement, IHtmlLinkElement
    {
        #region Fields

        IStyleSheet _sheet;
        String _buffer;
        ITokenList _relList;
        ISettableTokenList _sizes;
        Task _current;
        CancellationTokenSource _cts;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML link element.
        /// </summary>
        internal HTMLLinkElement()
            : base(Tags.Link, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _cts = new CancellationTokenSource();
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal Boolean IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the URI for the target resource.
        /// </summary>
        public String Href
        {
            get { return HyperRef(GetAttribute(AttributeNames.Href)).Href; }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        public String TargetLanguage
        {
            get { return GetAttribute(AttributeNames.HrefLang); }
            set { SetAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        public String Charset
        {
            get { return GetAttribute(AttributeNames.Charset); }
            set { SetAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the forward relationship of the linked resource from the document to the resource.
        /// </summary>
        public String Relation
        {
            get { return GetAttribute(AttributeNames.Rel); }
            set { SetAttribute(AttributeNames.Rel, value); }
        }

        /// <summary>
        /// Gets the list of relations contained in the rel attribute.
        /// </summary>
        public ITokenList RelationList
        {
            get { return _relList ?? (_relList = new TokenList(this, AttributeNames.Rel)); }
        }

        /// <summary>
        /// Gets the list of sizes defined in the sizes attribute.
        /// </summary>
        public ISettableTokenList Sizes
        {
            get { return _sizes ?? (_sizes = new SettableTokenList(this, AttributeNames.Sizes)); }
        }

        /// <summary>
        /// Gets or sets the reverse relationship of the linked resource from the document to the resource.
        /// </summary>
        public String Rev
        {
            get { return GetAttribute(AttributeNames.Rev); }
            set { SetAttribute(AttributeNames.Rev, value); }
        }

        /// <summary>
        /// Gets or sets if the stylesheet is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return Sheet.IsDisabled; }
            set { Sheet.IsDisabled = value; }
        }

        /// <summary>
        /// Gets or sets the name of the target frame to which the resource applies.
        /// </summary>
        public String Target
        {
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets the associated stylesheet.
        /// </summary>
        public IStyleSheet Sheet
        {
            get { return RelationList.Contains("stylesheet") ? _sheet : null; }
        }

        #endregion

        #region Internal methods

        internal override void Close()
        {
            base.Close();
            OnAttributeChanged(AttributeNames.Media, value =>
            {
                if (_sheet != null)
                    _sheet.Media.MediaText = value;
            });
            OnAttributeChanged(AttributeNames.Href, value => TargetChanged());
            OnAttributeChanged(AttributeNames.Type, value => TargetChanged());
            TargetChanged();
        }

        #endregion

        #region Helpers

        void TargetChanged()
        {
            if (Owner.Options.IsStyling)
            {
                var href = Href;

                if (href != null && _buffer != href && Owner != null)
                {
                    _buffer = href;
                    TryCancelCurrent();

                    _current = Owner.Options.LoadAsync(new Url(href), _cts.Token).ContinueWith(task =>
                    {
                        if (task.IsCompleted && !task.IsFaulted)
                            _sheet = Owner.Options.ParseStyling(source: task.Result, owner: this, type: Type);
                    });
                }
            }
        }

        void TryCancelCurrent()
        {
            if (_current != null && !_current.IsCompleted)
            {
                _cts.Cancel();
                _cts = new CancellationTokenSource();
            }
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// Specifies the possible values for the rel attribute.
        /// </summary>
        enum RelType : ushort
        {
            /// <summary>
            /// No particular relation.
            /// </summary>
            None,
            /// <summary>
            /// The rel=prefetch value.
            /// </summary>
            Prefetch,
            /// <summary>
            /// The rel=icon value.
            /// </summary>
            Icon,
            /// <summary>
            /// The rel=pingback value.
            /// </summary>
            Pingback,
            /// <summary>
            /// The rel=stylesheet value.
            /// </summary>
            Stylesheet,
            /// <summary>
            /// The rel=alternate value.
            /// </summary>
            Alternate,
            /// <summary>
            /// The rel=canonical value.
            /// </summary>
            Canonical,
            /// <summary>
            /// The rel=archives value.
            /// </summary>
            Archives,
            /// <summary>
            /// The rel=author value.
            /// </summary>
            Author,
            /// <summary>
            /// The rel=first value.
            /// </summary>
            First,
            /// <summary>
            /// The rel=help value.
            /// </summary>
            Help,
            /// <summary>
            /// The rel=sidebar value.
            /// </summary>
            Sidebar,
            /// <summary>
            /// The rel=tag value.
            /// </summary>
            Tag,
            /// <summary>
            /// The rel=search value.
            /// </summary>
            Search,
            /// <summary>
            /// The rel=index value.
            /// </summary>
            Index,
            /// <summary>
            /// The rel=license value.
            /// </summary>
            License,
            /// <summary>
            /// The rel=up value.
            /// </summary>
            Up,
            /// <summary>
            /// The rel=next value.
            /// </summary>
            Next,
            /// <summary>
            /// The rel=last value.
            /// </summary>
            Last,
            /// <summary>
            /// The rel=prev value.
            /// </summary>
            Prev
        }

        #endregion
    }
}

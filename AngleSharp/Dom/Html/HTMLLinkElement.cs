namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    sealed class HtmlLinkElement : HtmlElement, IHtmlLinkElement, IDisposable
    {
        #region Fields

        IStyleSheet _sheet;
        Url _buffer;
        TokenList _relList;
        SettableTokenList _sizes;
        Task _loadingTask;
        CancellationTokenSource _cts;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML link element.
        /// </summary>
        public HtmlLinkElement(Document owner)
            : base(owner, Tags.Link, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            RegisterAttributeObserver(AttributeNames.Media, UpdateMedia);
            RegisterAttributeObserver(AttributeNames.Disabled, UpdateDisabled);
            RegisterAttributeObserver(AttributeNames.Href, value => TargetChanged());
            RegisterAttributeObserver(AttributeNames.Type, value => TargetChanged());
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
        /// Gets the url of the link elements address.
        /// </summary>
        public Url Url
        {
            get { return this.HyperReference(GetAttribute(AttributeNames.Href)); }
        }

        /// <summary>
        /// Gets or sets the URI for the target resource.
        /// </summary>
        public String Href
        {
            get { return Url.Href; }
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
            get
            {
                if (_relList == null)
                {
                    _relList = new TokenList(GetAttribute(AttributeNames.Rel));
                    _relList.Changed += (s, ev) => UpdateAttribute(AttributeNames.Rel, _relList.ToString());
                }

                return _relList; 
            }
        }

        /// <summary>
        /// Gets the list of sizes defined in the sizes attribute.
        /// </summary>
        public ISettableTokenList Sizes
        {
            get
            {
                if (_sizes == null)
                {
                    _sizes = new SettableTokenList(GetAttribute(AttributeNames.Sizes));
                    _sizes.Changed += (s, ev) => UpdateAttribute(AttributeNames.Sizes, _sizes.Value);
                }

                return _sizes; 
            }
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
            get { return GetAttribute(AttributeNames.Disabled).ToBoolean(); }
            set { SetAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
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
            get { return _sheet; }
        }

        #endregion

        #region Internal methods

        void UpdateMedia(String value)
        {
            if (_sheet != null)
                _sheet.Media.MediaText = value;
        }

        void UpdateDisabled(String value)
        {
            if (_sheet != null)
                _sheet.IsDisabled = value != null;
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (_cts != null)
                _cts.Cancel();

            _cts = null;
            _loadingTask = null;
        }

        #endregion

        #region Helpers

        void TargetChanged()
        {
            if (Owner.Options.IsStyling)
            {
                if (_cts != null)
                    _cts.Cancel();

                var url = Url;

                if (url != null && (_buffer == null || !url.Equals(_buffer)))
                {
                    _buffer = url;
                    var requester = Owner.Options.GetRequester(url.Scheme);

                    if (requester == null)
                        return;

                    _cts = new CancellationTokenSource();
                    _loadingTask = LoadAsync(requester, url, _cts.Token);
                }
            }
        }

        async Task LoadAsync(IRequester requester, Url url, CancellationToken cancel)
        {
            var response = await requester.LoadAsync(url, _cts.Token).ConfigureAwait(false);

            if (response != null)
            {
                var options = new StyleOptions
                {
                    Element = this,
                    Title = Title,
                    IsDisabled = IsDisabled,
                    IsAlternate = RelationList.Contains(Keywords.Alternate)
                };
                _sheet = Owner.Options.ParseStyling(response, options, Type);
                response.Dispose();
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

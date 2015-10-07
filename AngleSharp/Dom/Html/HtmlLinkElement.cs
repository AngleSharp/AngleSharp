namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Services.Styling;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    sealed class HtmlLinkElement : HtmlElement, IHtmlLinkElement
    {
        #region Fields

        IStyleSheet _sheet;
        TokenList _relList;
        SettableTokenList _sizes;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML link element.
        /// </summary>
        public HtmlLinkElement(Document owner, String prefix = null)
            : base(owner, Tags.Link, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            RegisterAttributeObserver(AttributeNames.Media, UpdateMedia);
            RegisterAttributeObserver(AttributeNames.Disabled, UpdateDisabled);
            RegisterAttributeObserver(AttributeNames.Href, value => UpdateSource(value));
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
            get { return this.HyperReference(this.GetOwnAttribute(AttributeNames.Href)); }
        }

        /// <summary>
        /// Gets or sets the URI for the target resource.
        /// </summary>
        public String Href
        {
            get { return Url.Href; }
            set { this.SetOwnAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        public String TargetLanguage
        {
            get { return this.GetOwnAttribute(AttributeNames.HrefLang); }
            set { this.SetOwnAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        public String Charset
        {
            get { return this.GetOwnAttribute(AttributeNames.Charset); }
            set { this.SetOwnAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the forward relationship of the linked resource from the document to the resource.
        /// </summary>
        public String Relation
        {
            get { return this.GetOwnAttribute(AttributeNames.Rel); }
            set { this.SetOwnAttribute(AttributeNames.Rel, value); }
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
                    _relList = new TokenList(this.GetOwnAttribute(AttributeNames.Rel));
                    CreateBindings(_relList, AttributeNames.Rel);
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
                    _sizes = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sizes));
                    CreateBindings(_sizes, AttributeNames.Sizes);
                }

                return _sizes; 
            }
        }

        /// <summary>
        /// Gets or sets the reverse relationship of the linked resource from the document to the resource.
        /// </summary>
        public String Rev
        {
            get { return this.GetOwnAttribute(AttributeNames.Rev); }
            set { this.SetOwnAttribute(AttributeNames.Rev, value); }
        }

        /// <summary>
        /// Gets or sets if the stylesheet is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return this.GetOwnAttribute(AttributeNames.Disabled).ToBoolean(); }
            set { this.SetOwnAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the name of the target frame to which the resource applies.
        /// </summary>
        public String Target
        {
            get { return this.GetOwnAttribute(AttributeNames.Target); }
            set { this.SetOwnAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        public String Media
        {
            get { return this.GetOwnAttribute(AttributeNames.Media); }
            set { this.SetOwnAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets the associated stylesheet.
        /// </summary>
        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        #endregion

        #region Helpers

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

        void FinishLoading(Task<IResponse> task)
        {
            var type = Type ?? MimeTypes.Css;
            var config = Owner.Options;
            var engine = config.GetStyleEngine(type);

            if (task.IsCompleted && !task.IsFaulted)
            {
                using (var response = task.Result)
                {
                    if (engine != null && RelationList.Contains(Keywords.StyleSheet))
                    {
                        var options = new StyleOptions
                        {
                            Element = this,
                            Title = Title,
                            IsDisabled = IsDisabled,
                            IsAlternate = RelationList.Contains(Keywords.Alternate),
                            Configuration = config
                        };

                        if (response != null)
                        {
                            try { _sheet = engine.ParseStylesheet(response, options); }
                            catch { /* Do not care here */ }
                        }
                    }
                }
            }

            this.FireLoadOrErrorEvent(task);
        }

        void UpdateSource(String value)
        {
            this.CancelTasks();

            if (!String.IsNullOrEmpty(value))
            {
                var request = this.CreateRequestFor(Url);
                this.CreateTask(cancel => Owner.Loader.FetchAsync(request, cancel))
                    .ContinueWith(m => FinishLoading(m));
            }
        }

        #endregion
    }
}

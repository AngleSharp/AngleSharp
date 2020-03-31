namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html.LinkRels;
    using AngleSharp.Io;
    using System;

    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    sealed class HtmlLinkElement : HtmlElement, IHtmlLinkElement
    {
        #region Fields

        private BaseLinkRelation _relation;
        private TokenList _relList;
        private SettableTokenList _sizes;

        #endregion

        #region ctor

        public HtmlLinkElement(Document owner, String prefix = null)
            : base(owner, TagNames.Link, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Design properties

        internal Boolean IsVisited
        {
            get;
            set;
        }

        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload => _relation?.Processor?.Download;

        public String Href
        {
            get => this.GetUrlAttribute(AttributeNames.Href);
            set => this.SetOwnAttribute(AttributeNames.Href, value);
        }

        public String TargetLanguage
        {
            get => this.GetOwnAttribute(AttributeNames.HrefLang);
            set => this.SetOwnAttribute(AttributeNames.HrefLang, value);
        }

        public String Charset
        {
            get => this.GetOwnAttribute(AttributeNames.Charset);
            set => this.SetOwnAttribute(AttributeNames.Charset, value);
        }

        public String Relation
        {
            get => this.GetOwnAttribute(AttributeNames.Rel);
            set => this.SetOwnAttribute(AttributeNames.Rel, value);
        }

        public String ReverseRelation
        {
            get => this.GetOwnAttribute(AttributeNames.Rev);
            set => this.SetOwnAttribute(AttributeNames.Rev, value);
        }

        public String NumberUsedOnce
        {
            get => this.GetOwnAttribute(AttributeNames.Nonce);
            set => this.SetOwnAttribute(AttributeNames.Nonce, value);
        }

        public ITokenList RelationList
        {
            get
            {
                if (_relList == null)
                {
                    _relList = new TokenList(this.GetOwnAttribute(AttributeNames.Rel));
                    _relList.Changed += value => UpdateAttribute(AttributeNames.Rel, value);
                }

                return _relList;
            }
        }

        public ISettableTokenList Sizes
        {
            get
            {
                if (_sizes == null)
                {
                    _sizes = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sizes));
                    _sizes.Changed += value => UpdateAttribute(AttributeNames.Sizes, value);
                }

                return _sizes;
            }
        }

        public String Rev
        {
            get => this.GetOwnAttribute(AttributeNames.Rev);
            set => this.SetOwnAttribute(AttributeNames.Rev, value);
        }

        public Boolean IsDisabled
        {
            get => this.GetBoolAttribute(AttributeNames.Disabled);
            set => this.SetBoolAttribute(AttributeNames.Disabled, value);
        }

        public String Target
        {
            get => this.GetOwnAttribute(AttributeNames.Target);
            set => this.SetOwnAttribute(AttributeNames.Target, value);
        }

        public String Media
        {
            get => this.GetOwnAttribute(AttributeNames.Media);
            set => this.SetOwnAttribute(AttributeNames.Media, value);
        }

        public String Type
        {
            get => this.GetOwnAttribute(AttributeNames.Type);
            set => this.SetOwnAttribute(AttributeNames.Type, value);
        }

        public String Integrity
        {
            get => this.GetOwnAttribute(AttributeNames.Integrity);
            set => this.SetOwnAttribute(AttributeNames.Integrity, value);
        }

        public IStyleSheet Sheet
        {
            get
            {
                var sheetRelation = _relation as StyleSheetLinkRelation;
                return sheetRelation?.Sheet;
            }
        }

        public IDocument Import
        {
            get
            {
                var importRelation = _relation as ImportLinkRelation;
                return importRelation?.Import;
            }
        }

        public String CrossOrigin
        {
            get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
            set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            var rel = this.GetOwnAttribute(AttributeNames.Rel);

            if (rel != null)
            {
                _relList?.Update(rel);
                _relation = CreateFirstLegalRelation();
            }

            base.SetupElement();
        }

        internal void UpdateSizes(String value)
        {
            _sizes?.Update(value);
        }

        internal void UpdateMedia(String value)
        {
            var sheet = Sheet;

            if (sheet != null)
            {
                sheet.Media.MediaText = value;
            }
        }

        internal void UpdateDisabled(String value)
        {
            var sheet = Sheet;

            if (sheet != null)
            {
                sheet.IsDisabled = value != null;
            }
        }

        internal void UpdateSource(String value)
        {
            var task = _relation?.LoadAsync();
            Owner?.DelayLoad(task);
        }

        #endregion

        #region Helpers

        private BaseLinkRelation CreateFirstLegalRelation()
        {
            var relations = RelationList;
            var factory = Context?.GetFactory<ILinkRelationFactory>();

            foreach (var relation in relations)
            {
                var rel = factory?.Create(this, relation);

                if (rel != null)
                {
                    return rel;
                }
            }

            return null;
        }

        #endregion
    }
}

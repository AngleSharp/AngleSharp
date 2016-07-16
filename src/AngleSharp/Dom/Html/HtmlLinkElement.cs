namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Html.LinkRels;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using System;

    /// <summary>
    /// Represents the HTML link element.
    /// </summary>
    sealed class HtmlLinkElement : HtmlElement, IHtmlLinkElement
    {
        #region Fields

        BaseLinkRelation _relation;
        TokenList _relList;
        SettableTokenList _sizes;

        #endregion

        #region ctor

        static HtmlLinkElement()
        {
            RegisterCallback<HtmlLinkElement>(AttributeNames.Sizes, (element, value) => element.TryUpdate(element._sizes, value));
            RegisterCallback<HtmlLinkElement>(AttributeNames.Media, (element, value) => element.UpdateMedia(value));
            RegisterCallback<HtmlLinkElement>(AttributeNames.Disabled, (element, value) => element.UpdateDisabled(value));
            RegisterCallback<HtmlLinkElement>(AttributeNames.Href, (element, value) => element.UpdateSource(value));
            RegisterCallback<HtmlLinkElement>(AttributeNames.Rel, (element, value) => element.UpdateRelation(value));
        }

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

        public IDownload CurrentDownload
        {
            get 
            {
                var processor = _relation != null ? _relation.Processor : null;
                return processor != null ? processor.Download : null; 
            }
        }

        public String Href
        {
            get { return this.GetUrlAttribute(AttributeNames.Href); }
            set { this.SetOwnAttribute(AttributeNames.Href, value); }
        }

        public String TargetLanguage
        {
            get { return this.GetOwnAttribute(AttributeNames.HrefLang); }
            set { this.SetOwnAttribute(AttributeNames.HrefLang, value); }
        }

        public String Charset
        {
            get { return this.GetOwnAttribute(AttributeNames.Charset); }
            set { this.SetOwnAttribute(AttributeNames.Charset, value); }
        }

        public String Relation
        {
            get { return this.GetOwnAttribute(AttributeNames.Rel); }
            set { this.SetOwnAttribute(AttributeNames.Rel, value); }
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
            get { return this.GetOwnAttribute(AttributeNames.Rev); }
            set { this.SetOwnAttribute(AttributeNames.Rev, value); }
        }

        public Boolean IsDisabled
        {
            get { return this.GetOwnAttribute(AttributeNames.Disabled).ToBoolean(); }
            set { this.SetOwnAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        public String Target
        {
            get { return this.GetOwnAttribute(AttributeNames.Target); }
            set { this.SetOwnAttribute(AttributeNames.Target, value); }
        }

        public String Media
        {
            get { return this.GetOwnAttribute(AttributeNames.Media); }
            set { this.SetOwnAttribute(AttributeNames.Media, value); }
        }

        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        public IStyleSheet Sheet
        {
            get 
            { 
                var sheetRelation = _relation as StyleSheetLinkRelation;
                return sheetRelation != null ? sheetRelation.Sheet : null;
            }
        }

        public IDocument Import
        {
            get
            {
                var importRelation = _relation as ImportLinkRelation;
                return importRelation != null ? importRelation.Import : null;
            }
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();

            var rel = this.GetOwnAttribute(AttributeNames.Rel);

            if (rel != null)
            {
                UpdateRelation(rel);
            }
        }

        #endregion

        #region Helpers

        void UpdateMedia(String value)
        {
            var sheet = Sheet;

            if (sheet != null)
            {
                sheet.Media.MediaText = value;
            }
        }

        void UpdateDisabled(String value)
        {
            var sheet = Sheet;

            if (sheet != null)
            {
                sheet.IsDisabled = value != null;
            }
        }

        void UpdateRelation(String value)
        {
            TryUpdate(_relList, value);

            if (_relation != null)
            {
                //TODO
                //_relation.Cancel();
            }
            
            _relation = CreateFirstLegalRelation();
            UpdateSource(this.GetOwnAttribute(AttributeNames.Href));
        }

        void UpdateSource(String value)
        {
            var document = Owner;

            if (_relation != null && document != null)
            {
                //TODO
                //_relation.Cancel();

                var task = _relation.LoadAsync();
                document.DelayLoad(task);
            }
        }

        BaseLinkRelation CreateFirstLegalRelation()
        {
            var relations = RelationList;
            var factory = Owner.Options.GetFactory<ILinkRelationFactory>();

            foreach (var relation in relations)
            {
                var rel = factory.Create(this, relation);

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

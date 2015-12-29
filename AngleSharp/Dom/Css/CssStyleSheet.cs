namespace AngleSharp.Dom.Css
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS Stylesheet.
    /// </summary>
    sealed class CssStyleSheet : CssNode, ICssStyleSheet
    {
        #region Fields

        readonly MediaList _media;
        readonly String _url;
        readonly IElement _owner;
        readonly ICssStyleSheet _parent;
        readonly CssRuleList _rules;
        readonly CssParser _parser;
        readonly ICssRule _ownerRule;

        #endregion

        #region ctor

        internal CssStyleSheet(CssParser parser, String url, IElement owner)
        {
            _media = new MediaList(parser);
            _owner = owner;
            _url = url;
            _rules = new CssRuleList();
            _parser = parser;
            Children = _rules;
        }

        internal CssStyleSheet(CssParser parser, String url, ICssStyleSheet parent)
            : this(parser, url, parent != null ? parent.OwnerNode : null)
        {
            _parent = parent;
        }

        internal CssStyleSheet(CssParser parser)
            : this(parser, default(String), default(ICssStyleSheet))
        {
        }

        internal CssStyleSheet(CssRule ownerRule, String url)
            : this(ownerRule.Parser, url, ownerRule.Owner)
        {
            _ownerRule = ownerRule;
        }

        #endregion

        #region Properties

        public String Type
        {
            get { return MimeTypeNames.Css; }
        }

        public Boolean IsDisabled
        {
            get;
            set;
        }

        public IElement OwnerNode
        {
            get { return _owner; }
        }

        public ICssStyleSheet Parent
        {
            get { return _parent; }
        }

        public String Href
        {
            get { return _url; }
        }

        public String Title
        {
            get { return _owner != null ? _owner.GetAttribute(AttributeNames.Title) : null; }
        }

        public IMediaList Media
        {
            get { return _media; }
        }

        ICssRuleList ICssStyleSheet.Rules
        {
            get { return _rules; }
        }

        public ICssRule OwnerRule
        {
            get { return _ownerRule; }
        }

        #endregion

        #region Internal Properties

        internal CssRuleList Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Methods

        public override String ToCss(IStyleFormatter formatter)
        {
            return formatter.Sheet(_rules);
        }

        public void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        public Int32 Insert(String rule, Int32 index)
        {
            var value = _parser.ParseRule(rule);
            _rules.Insert(value, index, this, null);
            return index;            
        }

        #endregion

        #region Internal Methods

        internal void AddRule(CssRule rule)
        {
            if (rule != null)
            {
                _rules.Add(rule, this, null);
            }
        }

        #endregion
    }
}

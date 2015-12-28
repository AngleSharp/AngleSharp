namespace AngleSharp.Dom.Css
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS Stylesheet.
    /// </summary>
    sealed class CssStyleSheet : StyleSheet, ICssStyleSheet
    {
        #region Fields

        readonly CssRuleList _rules;
        readonly CssParser _parser;
        readonly ICssRule _ownerRule;

        #endregion

        #region ctor

        internal CssStyleSheet(CssParser parser, String url, IElement owner)
            : base(new MediaList(parser), url, owner)
        {
            _rules = new CssRuleList();
            _parser = parser;
            Children = _rules;
        }

        internal CssStyleSheet(CssParser parser, String url, IStyleSheet parent)
            : base(new MediaList(parser), url, parent)
        {
            _rules = new CssRuleList();
            _parser = parser;
            Children = _rules;
        }

        internal CssStyleSheet(CssParser parser)
            : this(parser, default(String), default(StyleSheet))
        {
        }

        internal CssStyleSheet(CssRule ownerRule, String url)
            : this(ownerRule.Parser, url, ownerRule.Owner)
        {
            _ownerRule = ownerRule;
        }

        #endregion

        #region Properties

        public override String Type
        {
            get { return MimeTypeNames.Css; }
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

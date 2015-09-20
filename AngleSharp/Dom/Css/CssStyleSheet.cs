namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

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

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        /// <param name="parser">The parser to use.</param>
        internal CssStyleSheet(CssParser parser)
            : this(parser, default(String), default(StyleSheet))
        {
        }

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        /// <param name="parser">The parser to use.</param>
        /// <param name="url">The url of the stylesheet.</param>
        /// <param name="owner">The owner element.</param>
        internal CssStyleSheet(CssParser parser, String url, IElement owner)
            : base(new MediaList(parser), url, owner)
        {
            _rules = new CssRuleList();
            _parser = parser;
        }

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        /// <param name="parser">The parser to use.</param>
        /// <param name="url">The url of the stylesheet.</param>
        /// <param name="parent">The parent stylesheet.</param>
        internal CssStyleSheet(CssParser parser, String url, IStyleSheet parent)
            : base(new MediaList(parser), url, parent)
        {
            _rules = new CssRuleList();
            _parser = parser;
        }

        /// <summary>
        /// Creates a new CSS Stylesheet owned by the given rule.
        /// </summary>
        /// <param name="ownerRule">The parent of the sheet.</param>
        /// <param name="url">The url of the stylesheet.</param>
        internal CssStyleSheet(CssRule ownerRule, String url)
            : this(ownerRule.Parser, url, ownerRule.Owner)
        {
            _ownerRule = ownerRule;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained children.
        /// </summary>
        public IEnumerable<ICssNode> Children
        {
            get { return _rules; }
        }

        /// <summary>
        /// Gets the CSS root node associated with the stylesheet.
        /// </summary>
        public CssNode ParseTree 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets the mime-type of the stylesheet, which is CSS.
        /// </summary>
        public override String Type
        {
            get { return MimeTypes.Css; }
        }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
        ICssRuleList ICssStyleSheet.Rules
        {
            get { return _rules; }
        }

        /// <summary>
        /// Gets the @import rule if the stylesheet was importated otherwise
        /// it returns null.
        /// </summary>
        public ICssRule OwnerRule
        {
            get { return _ownerRule; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
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

        /// <summary>
        /// Removes a style rule from the current style sheet object.
        /// </summary>
        /// <param name="index">
        /// The index representing the position to be removed.
        /// </param>
        /// <returns>The current stylesheet.</returns>
        public void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        /// <summary>
        /// Inserts a new style rule into the current style sheet.
        /// </summary>
        /// <param name="rule">
        /// A string containing the rule to be inserted (selector and 
        /// declaration).
        /// </param>
        /// <param name="index">
        /// The index representing the position to be inserted.
        /// </param>
        /// <returns>The current stylesheet.</returns>
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
                _rules.Add(rule, this, null);
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents a CSS Stylesheet.
    /// </summary>
    sealed class CSSStyleSheet : StyleSheet, ICssStyleSheet, ICssObject
    {
        #region Fields

        readonly CSSRuleList _rules;
        readonly ITextSource _source;

        ICssRule _ownerRule;
        IConfiguration _options;

        #endregion

        #region ctor
        
        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        internal CSSStyleSheet()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        /// <param name="source">The CSS source code.</param>
        internal CSSStyleSheet(String source)
            : this(new TextSource(source))
        {
        }

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        /// <param name="source">The underlying source.</param>
        internal CSSStyleSheet(ITextSource source)
        {
            _source = source;
            _rules = new CSSRuleList();
        }

        #endregion

        #region Properties

        public override String Type
        {
            get { return MimeTypes.Css; }
        }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
        internal CSSRuleList Rules
        {
            get { return _rules; }
        }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the style sheet.
        /// </summary>
        ICssRuleList ICssStyleSheet.Rules
        {
            get { return _rules; }
        }

        /// <summary>
        /// Gets the @import rule if the stylesheet was importated otherwise it returns null.
        /// </summary>
        public ICssRule OwnerRule
        {
            get { return _ownerRule; }
            internal set { _ownerRule = value; }
        }

        /// <summary>
        /// Gets the text stream source.
        /// </summary>
        internal ITextSource Source
        {
            get { return _source; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes a style rule from the current style sheet object.
        /// </summary>
        /// <param name="index">The index representing the position to be removed.</param>
        /// <returns>The current stylesheet.</returns>
        public void RemoveAt(Int32 index)
        {
            _rules.RemoveAt(index);
        }

        /// <summary>
        /// Inserts a new style rule into the current style sheet.
        /// </summary>
        /// <param name="rule">A string containing the rule to be inserted (selector and declaration).</param>
        /// <param name="index">The index representing the position to be inserted.</param>
        /// <returns>The current stylesheet.</returns>
        public Int32 Insert(String rule, Int32 index)
        {
            var value = CssParser.ParseRule(rule);
            _rules.Insert(value, index, this, null);
            return index;            
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var rule in _rules)
                sb.AppendLine(rule.ToCss());

            return sb.ToPool();
        }

        #endregion

        #region Internal Properties

        internal IConfiguration Options
        {
            get { return _options ?? Configuration.Default; }
            set { _options = value; }
        }

        #endregion
    }
}

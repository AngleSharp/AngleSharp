namespace AngleSharp.Dom.Css
{
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
        readonly TextSource _source;

        ICssRule _ownerRule;
        IConfiguration _options;

        #endregion

        #region ctor
        
        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        internal CssStyleSheet()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        /// <param name="source">The CSS source code.</param>
        internal CssStyleSheet(String source)
            : this(new TextSource(source))
        {
        }

        /// <summary>
        /// Creates a new CSS Stylesheet.
        /// </summary>
        /// <param name="source">The underlying source.</param>
        internal CssStyleSheet(TextSource source)
        {
            _source = source;
            _rules = new CssRuleList();
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
        internal CssRuleList Rules
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
        internal TextSource Source
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets a CSS code representation of the stylesheet.
        /// </summary>
        public String CssText
        {
            get
            {
                var sb = Pool.NewStringBuilder();

                foreach (var rule in _rules)
                    sb.AppendLine(rule.CssText);

                return sb.ToPool();
            }
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

        #region Internal Properties

        internal IConfiguration Options
        {
            get { return _options ?? Configuration.Default; }
            set { _options = value; }
        }

        #endregion
    }
}

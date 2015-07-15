namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents an unknown CSS rule.
    /// </summary>
    sealed class CssUnknownRule : CssRule
    {
        #region Fields

        readonly String _name;
        String _prelude;
        String _content;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unknown rule.
        /// </summary>
        public CssUnknownRule(String name, CssParser parser)
            : base(CssRuleType.Unknown, parser)
        {
            _name = name;
            _prelude = String.Empty;
            _content = String.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the @-rule.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the key text of the unknown rule.
        /// </summary>
        public String Prelude
        {
            get { return _prelude; }
            set { _prelude = value; }
        }

        public String Content
        {
            get { return _content; }
            set { _content = value; }
        }

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssUnknownRule;
            _prelude = newRule._prelude;
            _content = newRule._content;
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            return formatter.Rule("@" + _name, _prelude.Trim(), _content.Trim());
        }

        #endregion
    }
}

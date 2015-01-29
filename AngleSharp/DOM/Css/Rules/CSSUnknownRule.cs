namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an unknown CSS rule.
    /// </summary>
    sealed class CssUnknownRule : CssGroupingRule
    {
        #region Fields

        readonly String _name;
        String _prelude;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new unknown rule.
        /// </summary>
        public CssUnknownRule(String name)
            : base(CssRuleType.Unknown)
        {
            _name = name;
            _prelude = String.Empty;
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

        #endregion

        #region Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CssUnknownRule;
            _prelude = newRule._prelude;
            base.ReplaceWith(rule);
        }

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            var middle = _prelude.Length > 0 ? String.Concat(" ", _prelude, " ") : " ";
            return String.Concat("@", _name, middle, Rules.ToCssBlock());
        }

        #endregion
    }
}

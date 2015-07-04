namespace AngleSharp.Dom.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the @page rule.
    /// </summary>
    sealed class CssPageRule : CssRule, ICssPageRule
    {
        #region Fields

        readonly CssStyleDeclaration _style;
        ISelector _selector;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @page rule.
        /// </summary>
        internal CssPageRule()
            : base(CssRuleType.Page)
        {
            _style = new CssStyleDeclaration(this);
            _selector = SimpleSelector.All;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssPageRule)rule;
            _selector = newRule._selector;
            _style.Clear();
            _style.SetDeclarations(newRule._style);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selector for matching pages.
        /// </summary>
        public ISelector Selector
        {
            get { return _selector; }
            set { _selector = value; }
        }

        /// <summary>
        /// Gets the parsable textual representation of the page selector for the rule.
        /// </summary>
        public String SelectorText
        {
            get { return _selector.Text; }
            set
            {
                var selector = CssParser.ParseSelector(value);

                if (selector != null)
                    _selector = selector;
            }
        }

        /// <summary>
        /// Gets the  declaration-block of this rule.
        /// </summary>
        ICssStyleDeclaration ICssPageRule.Style
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets the  declaration-block of this rule.
        /// </summary>
        public CssStyleDeclaration Style
        {
            get { return _style; }
        }

        #endregion

        #region String representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(_style);
            return formatter.Rule("@page", _selector.Text, rules);
        }

        #endregion
    }
}

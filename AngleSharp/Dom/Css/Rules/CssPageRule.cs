namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

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
        internal CssPageRule(CssParser parser)
            : base(CssRuleType.Page, parser)
        {
            _style = new CssStyleDeclaration(this);
            _selector = SimpleSelector.All;
        }

        #endregion

        #region Methods

        public override IEnumerable<CssNode> GetChildren()
        {
            yield return _style;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssPageRule)rule;
            _selector = newRule._selector;
            _style.Clear();
            _style.SetDeclarations(newRule._style.Declarations);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selector for matching pages.
        /// </summary>
        public ISelector Selector
        {
            get { return _selector; }
            set { if (value != null) _selector = value; }
        }

        /// <summary>
        /// Gets the parsable textual representation of the page selector for the rule.
        /// </summary>
        public String SelectorText
        {
            get { return _selector.Text; }
            set
            {
                var selector = Parser.ParseSelector(value);

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

using AngleSharp.Css;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSSGroupingRule interface.
    /// </summary>
    public abstract class CSSGroupingRule : CSSRule
    {
        #region Members

        CSSRuleList _cssRules;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS grouping rule.
        /// </summary>
        internal CSSGroupingRule()
        {
            _cssRules = new CSSRuleList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all CSS rules contained within the media block.
        /// </summary>
        [DOM("cssRules")]
        public CSSRuleList CssRules
        {
            get { return _cssRules; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Used to insert a new rule into the media block.
        /// </summary>
        /// <param name="rule">The parsable text representing the rule. For rule sets this contains both the selector and the style declaration. For at-rules, this specifies both the at-identifier and the rule content.</param>
        /// <param name="index">The index within the media block's rule collection of the rule before which to insert the specified rule.</param>
        /// <returns>The index within the media block's rule collection of the newly inserted rule.</returns>
        [DOM("insertRule")]
        public Int32 InsertRule(String rule, Int32 index)
        {
            var obj = CssParser.ParseRule(rule);
            obj.ParentStyleSheet = _parent;
            obj.ParentRule = this;
            _cssRules.InsertAt(index, obj);
            return index;
        }

        /// <summary>
        /// Used to delete a rule from the media block.
        /// </summary>
        /// <param name="index">The index within the media block's rule collection of the rule to remove.</param>
        /// <returns>The current instance.</returns>
        [DOM("deleteRule")]
        public CSSGroupingRule DeleteRule(Int32 index)
        {
            if(index >= 0 && index < _cssRules.Length)
                _cssRules.RemoveAt(index);

            return this;
        }

        #endregion
    }
}

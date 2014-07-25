namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents an @keyframes rule.
    /// </summary>
    [DomName("CSSKeyframesRule")]
    sealed class CSSKeyframesRule : CSSRule, ICssRules
    {
        #region Fields

        readonly CSSRuleList _rules;

        String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @keyframes rule.
        /// </summary>
        internal CSSKeyframesRule()
        {
            _rules = new CSSRuleList();
            _type = CssRuleType.Keyframes;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the animation, used by the animation-name property.
        /// </summary>
        [DomName("name")]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the media rule.
        /// </summary>
        [DomName("cssRules")]
        public ICssRuleList Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new keyframe rule into the current CSSKeyframesRule.
        /// </summary>
        /// <param name="rule">A string containing a keyframe in the same format as an entry of a @keyframes at-rule.</param>
        /// <returns>The current @keyframes rule.</returns>
        [DomName("appendRule")]
        public CSSKeyframesRule AppendRule(String rule)
        {
            var obj = CssParser.ParseKeyframeRule(rule);

            if (obj == null)
                throw new DomException(ErrorCode.Syntax);

            obj.Owner = _ownerSheet;
            obj.Parent = this;
            _rules.List.Insert(_rules.Length, obj);
            return this;
        }

        /// <summary>
        /// Deletes a keyframe rule from the current CSSKeyframesRule. 
        /// </summary>
        /// <param name="key">The index of the keyframe to be deleted, expressed as a string resolving as a number between 0 and 1.</param>
        /// <returns>The current @keyframes rule.</returns>
        [DomName("deleteRule")]
        public CSSKeyframesRule DeleteRule(String key)
        {
            for (int i = 0; i < _rules.Length; i++)
            {
                if ((_rules[i] as CSSKeyframeRule).KeyText.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    _rules.List.RemoveAt(i);
                    break;
                }
            }

            return this;
        }

        /// <summary>
        /// Returns a keyframe rule corresponding to the given key.
        /// </summary>
        /// <param name="key">A string containing an index of the keyframe to be returned, resolving to a number between 0 and 1.</param>
        /// <returns>The keyframe or null.</returns>
        [DomName("findRule")]
        public CSSKeyframeRule FindRule(String key)
        {
            for (int i = 0; i < _rules.Length; i++)
            {
                var rule = _rules[i] as CSSKeyframeRule;

                if (rule.KeyText.Equals(key, StringComparison.OrdinalIgnoreCase))
                    return rule;
            }

            return null;
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSKeyframesRule;
            _name = newRule._name;
            _rules.List.Clear();
            _rules.List.AddRange(newRule._rules.List);
        }

        internal void AddRule(CSSKeyframeRule rule)
        {
            _rules.List.Add(rule);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@keyframes {0} {{{1}{2}}}", _name, Environment.NewLine, _rules.ToCss());
        }

        #endregion
    }
}

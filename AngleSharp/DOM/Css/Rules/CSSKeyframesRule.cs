namespace AngleSharp.DOM.Css
{
    using AngleSharp.Parser.Css;
    using System;
    using System.Linq;

    /// <summary>
    /// Represents an @keyframes rule.
    /// </summary>
    sealed class CSSKeyframesRule : CSSRule, ICssKeyframesRule
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
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the media rule.
        /// </summary>
        public CSSRuleList Rules
        {
            get { return _rules; }
        }

        ICssRuleList ICssKeyframesRule.Rules
        {
            get { return _rules; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new keyframe rule into the current CSSKeyframesRule.
        /// </summary>
        /// <param name="rule">A string containing a keyframe in the same format as an entry of a @keyframes at-rule.</param>
        public void Add(String rule)
        {
            var value = CssParser.ParseKeyframeRule(rule);
            _rules.Insert(value, _rules.Length);
            value.Owner = _ownerSheet;
            value.Parent = this;
        }

        /// <summary>
        /// Deletes a keyframe rule from the current CSSKeyframesRule. 
        /// </summary>
        /// <param name="key">The index of the keyframe to be deleted, expressed as a string resolving as a number between 0 and 1.</param>
        public void Remove(String key)
        {
            var element = Find(key);

            if (element == null)
                return;

            for (int i = 0; i < _rules.Length; i++)
            {
                if (element == _rules[i])
                {
                    _rules.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Returns a keyframe rule corresponding to the given key.
        /// </summary>
        /// <param name="key">A string containing an index of the keyframe to be returned, resolving to a number between 0 and 1.</param>
        /// <returns>The keyframe or null.</returns>
        public ICssKeyframeRule Find(String key)
        {
            return _rules.OfType<ICssKeyframeRule>().FirstOrDefault(m => m.KeyText.Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = rule as CSSKeyframesRule;
            _name = newRule._name;
            _rules.Clear();
            _rules.Import(newRule._rules, Owner, Parent);
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

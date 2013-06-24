using AngleSharp.Css;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an @keyframes rule.
    /// </summary>
    [DOM("CSSKeyframesRule")]
    public sealed class CSSKeyframesRule : CSSRule
    {
        #region Constants

        internal const String RuleName = "keyframes";

        #endregion

        #region Members

        CSSRuleList _cssRules;
        String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @keyframes rule.
        /// </summary>
        internal CSSKeyframesRule()
        {
            _cssRules = new CSSRuleList();
            _type = CssRule.Keyframes;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the animation, used by the animation-name property.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the media rule.
        /// </summary>
        [DOM("cssRules")]
        public CSSRuleList CssRules
        {
            get { return _cssRules; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a new keyframe rule into the current CSSKeyframesRule.
        /// </summary>
        /// <param name="rule">A string containing a keyframe in the same format as an entry of a @keyframes at-rule.</param>
        /// <returns>The current @keyframes rule.</returns>
        [DOM("appendRule")]
        public CSSKeyframesRule AppendRule(String rule)
        {
            var obj = CssParser.ParseKeyframeRule(rule);
            obj.ParentStyleSheet = _parent;
            obj.ParentRule = this;
            _cssRules.InsertAt(_cssRules.Length, obj);
            return this;
        }

        /// <summary>
        /// Deletes a keyframe rule from the current CSSKeyframesRule. 
        /// </summary>
        /// <param name="key">The index of the keyframe to be deleted, expressed as a string resolving as a number between 0 and 1.</param>
        /// <returns>The current @keyframes rule.</returns>
        [DOM("deleteRule")]
        public CSSKeyframesRule DeleteRule(String key)
        {
            for (int i = 0; i < _cssRules.Length; i++)
            {
                if ((_cssRules[i] as CSSKeyframeRule).KeyText.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    _cssRules.RemoveAt(i);
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
        [DOM("findRule")]
        public CSSKeyframeRule FindRule(String key)
        {
            for (int i = 0; i < _cssRules.Length; i++)
            {
                var rule = _cssRules[i] as CSSKeyframeRule;

                if (rule.KeyText.Equals(key, StringComparison.OrdinalIgnoreCase))
                    return rule;
            }

            return null;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@keyframes {0} {{{1}{2}}}", _name, Environment.NewLine, _cssRules.ToCss());
        }

        #endregion
    }
}

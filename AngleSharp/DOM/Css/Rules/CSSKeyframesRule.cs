using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an @keyframes rule.
    /// </summary>
    sealed class CSSKeyframesRule : CSSRule
    {
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
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the media rule.
        /// </summary>
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
        public CSSKeyframesRule AppendRule(string rule)
        {
            //TODO
            return this;
        }

        /// <summary>
        /// Deletes a keyframe rule from the current CSSKeyframesRule. 
        /// </summary>
        /// <param name="key">The index of the keyframe to be deleted, expressed as a string resolving as a number between 0 and 1.</param>
        /// <returns>The current @keyframes rule.</returns>
        public CSSKeyframesRule DeleteRule(string key)
        {
            //TODO
            return this;
        }

        /// <summary>
        /// Returns a keyframe rule corresponding to the given key.
        /// </summary>
        /// <param name="key">A string containing an index of the keyframe to be returned, resolving to a number between 0 and 1.</param>
        /// <returns>The keyframe or null.</returns>
        public CSSKeyframeRule FindRule(string key)
        {
            //TODO
            return null;
        }

        #endregion
    }
}

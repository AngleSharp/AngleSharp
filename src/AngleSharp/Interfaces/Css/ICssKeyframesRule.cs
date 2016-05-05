namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a complete set of keyframes for a single animation.
    /// </summary>
    [DomName("CSSKeyframesRule")]
    public interface ICssKeyframesRule : ICssRule
    {
        /// <summary>
        /// Gets or sets the name of the animation, used by the animation-name property.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets a CSSRuleList of the CSS rules in the media rule.
        /// </summary>
        [DomName("cssRules")]
        ICssRuleList Rules { get; }

        /// <summary>
        /// Inserts a new keyframe rule into the current CSSKeyframesRule.
        /// </summary>
        /// <param name="rule">
        /// A string containing a keyframe in the same format as an entry
        /// of a @keyframes at-rule.
        /// </param>
        [DomName("appendRule")]
        void Add(String rule);

        /// <summary>
        /// Deletes a keyframe rule from the current CSSKeyframesRule. 
        /// </summary>
        /// <param name="key">
        /// The index of the keyframe to be deleted, expressed as a string
        /// resolving as a number between 0 and 1.
        /// </param>
        [DomName("deleteRule")]
        void Remove(String key);

        /// <summary>
        /// Returns a keyframe rule corresponding to the given key.
        /// </summary>
        /// <param name="key">
        /// A string containing an index of the keyframe to be returned, 
        /// resolving to a number between 0 and 1.
        /// </param>
        /// <returns>The keyframe or null.</returns>
        [DomName("findRule")]
        ICssKeyframeRule Find(String key);
    }
}

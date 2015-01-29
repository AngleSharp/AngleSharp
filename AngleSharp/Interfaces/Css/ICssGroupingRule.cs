namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents any at (grouping) CSS rule.
    /// </summary>
    [DomName("CSSGroupingRule")]
    public interface ICssGroupingRule : ICssRule
    {
        /// <summary>
        /// Gets a list of all CSS rules contained within the grouping block.
        /// </summary>
        [DomName("cssRules")]
        ICssRuleList Rules { get; }

        /// <summary>
        /// Used to insert a new rule into the media block.
        /// </summary>
        /// <param name="rule">
        /// The parsable text representing the rule. For rule sets this contains both
        /// the selector and the style declaration. For at-rules, this specifies both
        /// the at-identifier and the rule content.</param>
        /// <param name="index">
        /// The index within the media block's rule collection of the rule before
        /// which to insert the specified rule.</param>
        /// <returns>
        /// The index within the media block's rule collection of the newly inserted rule.
        /// </returns>
        [DomName("insertRule")]
        Int32 Insert(String rule, Int32 index);

        /// <summary>
        /// Used to delete a rule from the media block.
        /// </summary>
        /// <param name="index">
        /// The index within the media block's rule collection of the rule to remove.
        /// </param>
        [DomName("deleteRule")]
        void RemoveAt(Int32 index);
    }
}

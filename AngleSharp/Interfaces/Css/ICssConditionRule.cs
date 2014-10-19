namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents all the "conditional" at-rules, which consist
    /// of a condition and a statement block.
    /// </summary>
    [DomName("CSSConditionRule")]
    public interface ICssConditionRule : ICssGroupingRule
    {
        /// <summary>
        /// Gets or sets the text representation of the condition.
        /// </summary>
        [DomName("conditionText")]
        String ConditionText { get; set; }
    }
}

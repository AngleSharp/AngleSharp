namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a @media CSS rule.
    /// </summary>
    [DomName("CSSMediaRule")]
    public interface ICssMediaRule : ICssConditionRule
    {
        /// <summary>
        /// Gets a list of media types for this rule.
        /// </summary>
        [DomName("media")]
        IMediaList Media { get; }
    }
}

namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;

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
        [DomPutForwards("mediaText")]
        IMediaList Media { get; }
    }
}

namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// Specifies a user's ability to hover over elements.
    /// </summary>
    public enum HoverAbility
    {
        /// <summary>
        /// Elements cannot be hovered at all.
        /// </summary>
        None,
        /// <summary>
        /// Possible, but requires a significant action on the user's part. 
        /// </summary>
        OnDemand,
        /// <summary>
        /// Hover over parts of the page is easily possible.
        /// </summary>
        Hover
    }
}

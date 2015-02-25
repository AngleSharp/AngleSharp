namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration with the various box sizing models / clip or origins.
    /// </summary>
    public enum BoxModel : ushort
    {
        /// <summary>
        /// The background extends to the outside edge of the border (but underneath the border in z-ordering).
        /// </summary>
        BorderBox,
        /// <summary>
        /// No background is drawn below the border (background extends to the outside edge of the padding).
        /// </summary>
        PaddingBox,
        /// <summary>
        /// The background is painted within (clipped to) the content box.
        /// </summary>
        ContentBox,
    }
}

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents the CSS vertical-align property.
    /// </summary>
    public interface ICssVerticalAlignProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected vertical alignment mode.
        /// </summary>
        VerticalAlignment Align { get; }

        /// <summary>
        /// Gets the alignment of of the element's baseline at the given length above
        /// the baseline of its parent or like absolute values, with the percentage
        /// being a percent of the line-height property.
        /// </summary>
        IDistance Shift { get; }
    }
}

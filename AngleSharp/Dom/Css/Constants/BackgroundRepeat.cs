namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// The enumeration over all possible repeat values.
    /// </summary>
    public enum BackgroundRepeat : ushort
    {
        /// <summary>
        /// The image is repeated in the given direction as much as needed to cover the whole
        /// background image painting area. The last image may be clipped if the whole thing
        /// won't fit in the remaining area.
        /// </summary>
        Repeat,
        /// <summary>
        /// The image is repeated in the given direction as much as needed to cover most of
        /// the background image painting area, without clipping an image. The remaining
        /// non-covered space is spaced out evenly between the images. The first and last
        /// images touches the edge of the element. The value of the background-position CSS
        /// property is ignored for the concerned direction, except if one single image is
        /// greater than the background image painting area, which is the only case where an
        /// image can be clipped when the space value is used.
        /// </summary>
        Space,
        /// <summary>
        /// The image is repeated in the given direction as much as needed to cover most of
        /// the background image painting area, without clipping an image. If it doesn't cover
        /// exactly the area, the tiles are resized in that direction in order to match it.
        /// </summary>
        Round,
        /// <summary>
        /// The image is not repeated (and hence the background image painting area will not
        /// necessarily been entirely covered). The position of the non-repeated background
        /// image is defined by the background-position CSS property.
        /// </summary>
        NoRepeat
    }
}

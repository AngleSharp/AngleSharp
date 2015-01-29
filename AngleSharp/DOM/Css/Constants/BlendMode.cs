namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration over all possible blend-mode values.
    /// </summary>
    public enum BlendMode
    {
        /// <summary>
        /// Indicates that no blending will happen: the selected color is the
        /// one of the image on the top, and not the one of the backdrop.
        /// </summary>
        Normal,
        /// <summary>
        /// Indicates that the blended color is the multiplication of the front
        /// color and the one of the background. Multiplication of colors always
        /// result in a color as dark as the original ones. To the limit,
        /// multiplying with black results in black, and multiplying with white
        /// results in the other original color.
        /// </summary>
        Multiply,
        /// <summary>
        /// Simulates the simultaneous projection of the two images on a screen:
        /// the resulting color is always as light as the original ones. To the
        /// limit, multiplying with black results in the other original color, and
        /// multiplying with white results in white.
        /// </summary>
        Screen,
        /// <summary>
        /// Acts as multiply or screen depending of the color of the background
        /// image for a given pixel. This is similar to hard-light but with the
        /// top and bottom color swapped.
        /// </summary>
        Overlay,
        /// <summary>
        /// Selects, for each pixel, the darkest color of the backdrop or the
        /// front image.
        /// </summary>
        Darken,
        /// <summary>
        /// Selects, for each pixel, the lightest color of the backdrop or the
        /// front image.
        /// </summary>
        Lighten,
        /// <summary>
        /// Lightens the backdrop according to the value of the foreground color:
        /// the brighter it is, the more its color affects the backdrop. White on
        /// the foreground image leads to white.
        /// </summary>
        ColorDodge,
        /// <summary>
        /// Darkens the backdrop according to the value of the foreground color:
        /// the darker it is, the more its color affects the backdrop. Black on
        /// the foreground image leads to black.
        /// </summary>
        ColorBurn,
        /// <summary>
        /// Acts as multiply or screen depending of the color of the foreground
        /// image for a given pixel. It reproduces an effect next to shining a
        /// harsh spotlight on the backdrop. This is similar to overlay but with
        /// the top and bottom color swapped.
        /// </summary>
        HardLight,
        /// <summary>
        /// Similar to hard-light and darkens  or lightens the colors, depending of
        /// the foreground color. It leads to an effect next to shining a diffused
        /// spotlight on the backdrop.
        /// </summary>
        SoftLight,
        /// <summary>
        /// Substracts the darker color to the lighter one. Black on the foreground
        /// has therefore no effect, white leads to the color of the backdrop, inverted.
        /// </summary>
        Difference,
        /// <summary>
        /// Similar to difference but produce a result with less contrast. Like for
        /// difference,  black on the foreground has no effect, while white leads to
        /// the color of the backdrop, inverted.
        /// </summary>
        Exclusion,
        /// <summary>
        /// Keeps the hue of the foreground color, while using the saturation and
        /// luminosity of the backdrop.
        /// </summary>
        Hue,
        /// <summary>
        /// Keeps the saturation of the foreground color, while using the hue and
        /// luminosity of the backdrop. A backdrop with no saturation, that is a pure
        /// grey, will lead to no change to the foreground image.
        /// </summary>
        Saturation,
        /// <summary>
        /// Keeps the saturation and hue of the foreground color, while using the
        /// luminosity of the backdrop. This preserves gray levels and can be used to
        /// colorize the foreground image.
        /// </summary>
        Color,
        /// <summary>
        /// Keeps the luminosity of the foreground color, while using the saturation
        /// and hue of the backdrop.
        /// </summary>
        Luminosity
    }
}

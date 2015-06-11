namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More infos can be found on the W3C homepage or
    /// in condensed form at 
    /// http://css-infos.net/property/box-decoration-break
    /// Gets if each box is independently wrapped with the border
    /// and padding. Otherwise no border and no padding are inserted
    /// at the break.
    /// </summary>
    sealed class CssBoxDecorationBreak : CssProperty
    {
        #region ctor

        internal CssBoxDecorationBreak()
            : base(PropertyNames.BoxDecorationBreak)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: false
            get { return Converters.BoxDecorationConverter; }
        }

        #endregion
    }
}

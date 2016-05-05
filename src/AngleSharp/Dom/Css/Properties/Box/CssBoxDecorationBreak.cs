namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

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
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.BoxDecorationConverter.OrDefault(false);

        #endregion

        #region ctor

        internal CssBoxDecorationBreak()
            : base(PropertyNames.BoxDecorationBreak)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}

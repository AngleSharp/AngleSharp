namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right
    /// </summary>
    sealed class CssBorderRightProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = CssBorderProperty.StyleConverter;

        #endregion

        #region ctor

        internal CssBorderRightProperty()
            : base(PropertyNames.BorderRight, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return CssBorderProperty.StyleConverter; }
        }

        #endregion
    }
}

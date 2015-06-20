namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CssPaddingProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.Periodic(
            PropertyNames.PaddingTop, PropertyNames.PaddingRight, PropertyNames.PaddingBottom, PropertyNames.PaddingLeft).OrDefault(Length.Zero);

        #endregion

        #region ctor

        internal CssPaddingProperty()
            : base(PropertyNames.Padding)
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

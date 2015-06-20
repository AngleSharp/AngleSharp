namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-style
    /// </summary>
    sealed class CssBorderStyleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.Periodic(
            PropertyNames.BorderTopStyle, PropertyNames.BorderRightStyle, PropertyNames.BorderBottomStyle, PropertyNames.BorderLeftStyle).OrDefault();

        #endregion

        #region ctor

        internal CssBorderStyleProperty()
            : base(PropertyNames.BorderStyle)
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

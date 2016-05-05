namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-style
    /// </summary>
    sealed class CssBorderRightStyleProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.LineStyleConverter.OrDefault(LineStyle.None);

        #endregion

        #region ctor

        internal CssBorderRightStyleProperty()
            : base(PropertyNames.BorderRightStyle)
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

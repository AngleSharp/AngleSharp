namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left-color
    /// </summary>
    sealed class CssBorderLeftColorProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.CurrentColorConverter.OrDefault(Color.Transparent);

        #endregion

        #region ctor

        internal CssBorderLeftColorProperty()
            : base(PropertyNames.BorderLeftColor)
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

namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style
    /// </summary>
    sealed class CssListStyleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.ListStyleConverter.Option().For(PropertyNames.ListStyleType),
            Converters.ListPositionConverter.Option().For(PropertyNames.ListStylePosition),
            Converters.OptionalImageSourceConverter.Option().For(PropertyNames.ListStyleImage)).OrDefault();

        #endregion

        #region ctor

        internal CssListStyleProperty()
            : base(PropertyNames.ListStyle, PropertyFlags.Inherited)
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

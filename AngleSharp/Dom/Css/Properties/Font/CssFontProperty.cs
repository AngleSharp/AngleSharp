namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font
    /// </summary>
    sealed class CssFontProperty : CssShorthandProperty
    {
        #region Fields

        //[ [ <‘font-style’> || <font-variant-css21> || <‘font-weight’> || <‘font-stretch’> ]? 
        // <‘font-size’> [ / <‘line-height’> ]? <‘font-family’> ] | 
        // caption | icon | menu | message-box | small-caption | status-bar

        static readonly IValueConverter StyleConverter = Converters.WithOrder(
            Converters.WithAny(
                Converters.FontStyleConverter.Option().For(PropertyNames.FontStyle),
                Converters.FontVariantConverter.Option().For(PropertyNames.FontVariant),
                Converters.FontWeightConverter.Or(Converters.WeightIntegerConverter).Option().For(PropertyNames.FontWeight),
                Converters.FontStretchConverter.Option().For(PropertyNames.FontStretch)),
            Converters.WithOrder(
                Converters.FontSizeConverter.Required().For(PropertyNames.FontSize),
                Converters.LineHeightConverter.StartsWithDelimiter().Option().For(PropertyNames.LineHeight),
            Converters.FontFamiliesConverter.Required().For(PropertyNames.FontFamily))).Or(
        Converters.SystemFontConverter);

        #endregion

        #region ctor

        internal CssFontProperty()
            : base(PropertyNames.Font, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Instead of specifying individual longhand properties, a keyword
        /// can be used to represent a specific system font.
        /// </summary>
        /// <param name="font">The font to select.</param>
        static void SetSystemFont(SystemFont font)
        {
            switch (font)
            {
                case SystemFont.Caption:
                case SystemFont.Icon:
                case SystemFont.MessageBox:
                    //SetFont("Arial", "16px");
                    break;
                case SystemFont.StatusBar:
                case SystemFont.Menu:
                    //SetFont("Segoe UI", "12px");
                    break;
                case SystemFont.SmallCaption:
                    //SetFont("Segoe UI", "15px");
                    break;
            }
        }

        #endregion
    }
}

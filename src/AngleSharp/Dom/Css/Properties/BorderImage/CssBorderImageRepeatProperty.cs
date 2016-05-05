namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-repeat
    /// </summary>
    sealed class CssBorderImageRepeatProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter TheConverter = Map.BorderRepeatModes.ToConverter().Many(1, 2);
        static readonly IValueConverter StyleConverter = TheConverter.OrDefault(BorderRepeat.Stretch);

        #endregion

        #region ctor

        internal CssBorderImageRepeatProperty()
            : base(PropertyNames.BorderImageRepeat)
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

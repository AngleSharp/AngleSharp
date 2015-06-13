namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-spacing
    /// </summary>
    sealed class CssBorderSpacingProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter SpacingConverter = Converters.LengthConverter.Many(1, 2);

        #endregion

        #region ctor

        internal CssBorderSpacingProperty()
            : base(PropertyNames.BorderSpacing, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: Length.Zero
            get { return SpacingConverter; }
        }

        #endregion
    }
}

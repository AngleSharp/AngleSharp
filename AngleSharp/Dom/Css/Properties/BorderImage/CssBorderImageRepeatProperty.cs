namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-repeat
    /// </summary>
    sealed class CssBorderImageRepeatProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<BorderRepeat[]> Converter = 
            Map.BorderRepeatModes.ToConverter().Many(1, 2);

        #endregion

        #region ctor

        internal CssBorderImageRepeatProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderImageRepeat, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BorderRepeat.Stretch;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

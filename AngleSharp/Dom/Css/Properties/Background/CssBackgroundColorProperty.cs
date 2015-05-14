namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-color
    /// Gets the color of the background.
    /// </summary>
    sealed class CssBackgroundColorProperty : CssProperty
    {
        #region ctor

        internal CssBackgroundColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundColor, rule, PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Color.Transparent;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.CurrentColorConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.CurrentColorConverter.Validate(value);
        }

        #endregion
    }
}

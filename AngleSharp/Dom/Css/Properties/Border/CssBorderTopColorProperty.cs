namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-color
    /// </summary>
    sealed class CssBorderTopColorProperty : CssProperty
    {
        #region ctor

        internal CssBorderTopColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTopColor, rule)
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

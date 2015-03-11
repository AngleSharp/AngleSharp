namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-inside
    /// or even better
    /// http://dev.w3.org/csswg/css-break/#break-inside
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakInsideProperty : CssProperty
    {
        #region ctor

        internal CssBreakInsideProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BreakInside, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BreakMode.Auto;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.BreakInsideModeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.BreakInsideModeConverter.Validate(value);
        }

        #endregion
    }
}

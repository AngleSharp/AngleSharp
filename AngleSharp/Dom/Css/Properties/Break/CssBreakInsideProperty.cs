namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

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

        internal CssBreakInsideProperty()
            : base(PropertyNames.BreakInside)
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

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.BreakInsideModeConverter.Validate(value);
        }

        #endregion
    }
}

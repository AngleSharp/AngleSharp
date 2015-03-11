namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-after
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakAfterProperty : CssProperty
    {
        #region ctor

        internal CssBreakAfterProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BreakAfter, rule)
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
            return Converters.BreakModeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.BreakModeConverter.Validate(value);
        }

        #endregion
    }
}

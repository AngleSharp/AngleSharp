namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-before
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssBreakBeforeProperty : CssProperty
    {
        #region ctor

        internal CssBreakBeforeProperty()
            : base(PropertyNames.BreakBefore)
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

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.BreakModeConverter.Validate(value);
        }

        #endregion
    }
}

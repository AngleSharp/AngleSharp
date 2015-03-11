namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// Gets the currently selected position mode.
    /// </summary>
    sealed class CssPositionProperty : CssProperty
    {
        #region ctor

        internal CssPositionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Position, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return PositionMode.Static;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.PositionModeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.PositionModeConverter.Validate(value);
        }

        #endregion
    }
}

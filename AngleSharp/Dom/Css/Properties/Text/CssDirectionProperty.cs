namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/direction
    /// Gets the selected text direction.
    /// </summary>
    sealed class CssDirectionProperty : CssProperty
    {
        #region ctor

        internal CssDirectionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Direction, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return DirectionMode.Ltr;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.DirectionModeConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.DirectionModeConverter.Validate(value);
        }

        #endregion
    }
}

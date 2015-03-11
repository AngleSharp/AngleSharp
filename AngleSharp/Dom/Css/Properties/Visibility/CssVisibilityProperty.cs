namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// Gets the visibility mode.
    /// </summary>
    sealed class CssVisibilityProperty : CssProperty
    {
        #region ctor

        internal CssVisibilityProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Visibility, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Visibility.Visible;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.VisibilityConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.VisibilityConverter.Validate(value);
        }

        #endregion
    }
}

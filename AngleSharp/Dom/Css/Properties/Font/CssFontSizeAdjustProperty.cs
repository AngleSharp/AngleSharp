namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// http://dev.w3.org/csswg/css-fonts/#propdef-font-size-adjust
    /// Gets the aspect value specified by the property, if any.
    /// </summary>
    sealed class CssFontSizeAdjustProperty : CssProperty
    {
        #region ctor

        internal CssFontSizeAdjustProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontSizeAdjust, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.OptionalNumberConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.OptionalNumberConverter.Validate(value);
        }

        #endregion
    }
}

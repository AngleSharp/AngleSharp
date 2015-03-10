namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-indent
    /// Gets the indentation, which is either a percentage of the
    /// containing block width or specified as fixed length. Negative
    /// values are allowed.
    /// </summary>
    sealed class CssTextIndentProperty : CssProperty
    {
        #region ctor

        internal CssTextIndentProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextIndent, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.LengthOrPercentConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LengthOrPercentConverter.Validate(value);
        }

        #endregion
    }
}

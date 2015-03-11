namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align
    /// Gets the alignment of of the element's baseline at the given length
    /// above the baseline of its parent or like absolute values, with the
    /// percentage being a percent of the line-height property.
    /// Gets the selected vertical alignment mode.
    /// </summary>
    sealed class CssVerticalAlignProperty : CssProperty
    {
        #region ctor

        internal CssVerticalAlignProperty(CssStyleDeclaration rule)
            : base(PropertyNames.VerticalAlign, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return VerticalAlignment.Baseline;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.VerticalAlignmentConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LengthOrPercentConverter.Validate(value) || 
                   Converters.VerticalAlignmentConverter.Validate(value);
        }

        #endregion
    }
}

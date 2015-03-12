namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-gap
    /// Gets the selected width of gaps between columns.
    /// </summary>
    sealed class CssColumnGapProperty : CssProperty
    {
        #region ctor

        internal CssColumnGapProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnGap, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return new Length(1f, Length.Unit.Em);
        }

        protected override Object Compute(IElement element)
        {
            return Converters.LengthOrNormalConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.LengthOrNormalConverter.Validate(value);
        }

        #endregion
    }
}

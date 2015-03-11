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
        #region Fields

        static readonly IValueConverter<Length> Converter = 
            Converters.LengthConverter.Or(Keywords.Normal, new Length(1f, Length.Unit.Em));

        #endregion

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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

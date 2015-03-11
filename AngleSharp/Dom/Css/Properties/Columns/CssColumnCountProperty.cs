namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-count
    /// Gets the number of columns.
    /// </summary>
    sealed class CssColumnCountProperty : CssProperty
    {
        #region ctor

        internal CssColumnCountProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnCount, rule, PropertyFlags.Animatable)
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
            return Converters.OptionalIntegerConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.OptionalIntegerConverter.Validate(value);
        }

        #endregion
    }
}

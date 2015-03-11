namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// Gets the width of a single columns.
    /// </summary>
    sealed class CssColumnWidthProperty : CssProperty
    {
        #region ctor

        internal CssColumnWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnWidth, rule, PropertyFlags.Animatable)
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
            return Converters.AutoLengthConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.AutoLengthConverter.Validate(value);
        }

        #endregion
    }
}

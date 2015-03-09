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
        #region Fields

        internal static readonly IValueConverter<Int32?> Converter = 
            Converters.IntegerConverter.OrNullDefault();

        #endregion

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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

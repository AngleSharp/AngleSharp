namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CssColumnsProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue>> Converter = 
            Converters.WithAny(
                Converters.AutoLengthConverter.Val().Option(),
                Converters.OptionalIntegerConverter.Val().Option());

        #endregion

        #region ctor

        internal CssColumnsProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Columns, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssColumnWidthProperty>().TrySetValue(m.Item1);
                Get<CssColumnCountProperty>().TrySetValue(m.Item2);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var width = properties.OfType<CssColumnWidthProperty>().FirstOrDefault();
            var count = properties.OfType<CssColumnCountProperty>().FirstOrDefault();

            if (width == null || count == null)
                return String.Empty;
            else if (!width.HasValue || !count.HasValue)
                return String.Empty;

            return String.Concat(width.SerializeValue(), " ", count.SerializeValue());
        }

        #endregion
    }
}

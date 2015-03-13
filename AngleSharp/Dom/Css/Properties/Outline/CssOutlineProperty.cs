namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CssOutlineProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue>> Converter = 
            Converters.WithAny(
                Converters.LineWidthConverter.Val().Option(),
                Converters.LineStyleConverter.Val().Option(),
                Converters.InvertedColorConverter.Val().Option());

        #endregion

        #region ctor

        internal CssOutlineProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Outline, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssOutlineWidthProperty>().TrySetValue(m.Item1);
                Get<CssOutlineStyleProperty>().TrySetValue(m.Item2);
                Get<CssOutlineColorProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var width = properties.OfType<CssOutlineWidthProperty>().FirstOrDefault();
            var style = properties.OfType<CssOutlineStyleProperty>().FirstOrDefault();
            var color = properties.OfType<CssOutlineColorProperty>().FirstOrDefault();

            if (width == null || style == null || color == null)
                return String.Empty;

            var result = new List<String>();

            if (width.HasValue)
                result.Add(width.SerializeValue());

            if (color.HasValue)
                result.Add(color.SerializeValue());

            if (style.HasValue)
                result.Add(style.SerializeValue());

            return String.Join(" ", result);
        }

        #endregion
    }
}

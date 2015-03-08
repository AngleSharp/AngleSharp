namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    sealed class CssTextDecorationProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue>> Converter = 
            Converters.WithAny(
                CssTextDecorationColorProperty.Converter.Val().Option(),
                CssTextDecorationStyleProperty.Converter.Val().Option(),
                CssTextDecorationLineProperty.Converter.Val().Option());

        #endregion

        #region ctor

        internal CssTextDecorationProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TextDecoration, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssTextDecorationColorProperty>().TrySetValue(m.Item1);
                Get<CssTextDecorationStyleProperty>().TrySetValue(m.Item2);
                Get<CssTextDecorationLineProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var color = properties.OfType<CssTextDecorationColorProperty>().FirstOrDefault();
            var style = properties.OfType<CssTextDecorationStyleProperty>().FirstOrDefault();
            var line = properties.OfType<CssTextDecorationLineProperty>().FirstOrDefault();

            if (color == null || style == null || line == null)
                return String.Empty;

            return String.Format("{0} {1} {2}", line.SerializeValue(), style.SerializeValue(), color.SerializeValue());
        }

        #endregion
    }
}

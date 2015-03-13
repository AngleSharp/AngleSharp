namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image
    /// </summary>
    sealed class CssBorderImageProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, Tuple<ICssValue, ICssValue, ICssValue>, ICssValue>> Converter = 
            Converters.WithAny(
                Converters.OptionalImageSourceConverter.Val().Option(),
                Converters.WithOrder(
                    CssBorderImageSliceProperty.Converter.Val().Option(),
                    CssBorderImageWidthProperty.Converter.Val().StartsWithDelimiter().Option(),
                    CssBorderImageOutsetProperty.Converter.Val().StartsWithDelimiter().Option()),
                CssBorderImageRepeatProperty.Converter.Val().Option());

        #endregion

        #region ctor

        internal CssBorderImageProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderImage, rule)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssBorderImageSourceProperty>().TrySetValue(m.Item1);
                Get<CssBorderImageSliceProperty>().TrySetValue(m.Item2.Item1);
                Get<CssBorderImageWidthProperty>().TrySetValue(m.Item2.Item2);
                Get<CssBorderImageOutsetProperty>().TrySetValue(m.Item2.Item3);
                Get<CssBorderImageRepeatProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var source = properties.OfType<CssBorderImageSourceProperty>().FirstOrDefault();
            var slice = properties.OfType<CssBorderImageSliceProperty>().FirstOrDefault();
            var width = properties.OfType<CssBorderImageWidthProperty>().FirstOrDefault();
            var outset = properties.OfType<CssBorderImageOutsetProperty>().FirstOrDefault();
            var repeat = properties.OfType<CssBorderImageRepeatProperty>().FirstOrDefault();

            if (source == null || slice == null || width == null || outset == null || repeat == null)
                return String.Empty;

            var values = new List<String>();
            values.Add(source.SerializeValue());

            if (slice.HasValue)
                values.Add(slice.SerializeValue());

            if (width.HasValue || outset.HasValue)
            {
                values.Add("/");

                if (width.HasValue)
                    values.Add(width.SerializeValue());

                if (outset.HasValue)
                {
                    values.Add("/");
                    values.Add(outset.SerializeValue());
                }
            }

            if (repeat.HasValue)
                values.Add(repeat.SerializeValue());

            return String.Format(" ", values);
        }

        #endregion
    }
}

namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style
    /// </summary>
    sealed class CssListStyleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue>> Converter = 
            Converters.WithAny(
                Converters.ListStyleConverter.Val().Option(null).Val(),
                Converters.ListPositionConverter.Val().Option(null).Val(),
                Converters.OptionalImageSourceConverter.Val().Option(null).Val());

        #endregion

        #region ctor

        internal CssListStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ListStyle, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssListStyleTypeProperty>().TrySetValue(m.Item1);
                Get<CssListStylePositionProperty>().TrySetValue(m.Item2);
                Get<CssListStyleImageProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var type = properties.OfType<CssListStyleTypeProperty>().FirstOrDefault();
            var position = properties.OfType<CssListStylePositionProperty>().FirstOrDefault();
            var image = properties.OfType<CssListStyleImageProperty>().FirstOrDefault();

            if (type == null || position == null || image == null)
                return String.Empty;

            var result = Pool.NewStringBuilder();
            result.Append(type.SerializeValue());

            if (image.HasValue)
                result.Append(' ').Append(image.SerializeValue());

            if (position.HasValue)
                result.Append(' ').Append(position.SerializeValue());

            return result.ToPool();
        }

        #endregion
    }
}

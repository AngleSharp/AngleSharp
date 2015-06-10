namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style
    /// </summary>
    sealed class CssListStyleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<CssValue, CssValue, CssValue>> StyleConverter = 
            Converters.WithAny(
                Converters.ListStyleConverter.Val().Option().Val(),
                Converters.ListPositionConverter.Val().Option().Val(),
                Converters.OptionalImageSourceConverter.Val().Option().Val());

        #endregion

        #region ctor

        internal CssListStyleProperty()
            : base(PropertyNames.ListStyle, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.Validate(value);
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssListStyleTypeProperty>().TrySetValue(m.Item1);
                Get<CssListStylePositionProperty>().TrySetValue(m.Item2);
                Get<CssListStyleImageProperty>().TrySetValue(m.Item3);
            });*/
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var type = properties.OfType<CssListStyleTypeProperty>().FirstOrDefault();
            var position = properties.OfType<CssListStylePositionProperty>().FirstOrDefault();
            var image = properties.OfType<CssListStyleImageProperty>().FirstOrDefault();

            if (type == null || position == null || image == null)
                return String.Empty;

            var result = new List<String>();

            if (type.HasValue)
                result.Add(type.SerializeValue());

            if (image.HasValue)
                result.Add(image.SerializeValue());

            if (position.HasValue)
                result.Add(position.SerializeValue());

            return String.Join(" ", result);
        }

        #endregion
    }
}

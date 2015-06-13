namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline
    /// </summary>
    sealed class CssOutlineProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.WithAny(
            Converters.LineWidthConverter.Option(),
            Converters.LineStyleConverter.Option(),
            Converters.InvertedColorConverter.Option());

        #endregion

        #region ctor

        internal CssOutlineProperty()
            : base(PropertyNames.Outline, PropertyFlags.Animatable)
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
            return StyleConverter.Convert(value) != null;
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssOutlineWidthProperty>().TrySetValue(m.Item1);
                Get<CssOutlineStyleProperty>().TrySetValue(m.Item2);
                Get<CssOutlineColorProperty>().TrySetValue(m.Item3);
            });*/
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

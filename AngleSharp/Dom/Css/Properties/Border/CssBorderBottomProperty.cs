namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom
    /// </summary>
    sealed class CssBorderBottomProperty : CssShorthandProperty
    {
        #region ctor

        internal CssBorderBottomProperty()
            : base(PropertyNames.BorderBottom, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return CssBorderProperty.StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            return CssBorderProperty.StyleConverter.TryConvert(value, m =>
            {
                Get<CssBorderBottomWidthProperty>().TrySetValue(m.Item1);
                Get<CssBorderBottomStyleProperty>().TrySetValue(m.Item2);
                Get<CssBorderBottomColorProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var color = properties.OfType<CssBorderBottomColorProperty>().FirstOrDefault();
            var width = properties.OfType<CssBorderBottomWidthProperty>().FirstOrDefault();
            var style = properties.OfType<CssBorderBottomStyleProperty>().FirstOrDefault();

            if (color == null || width == null || style == null)
                return String.Empty;

            return CssBorderProperty.SerializeValue(width, style, color);
        }

        #endregion
    }
}

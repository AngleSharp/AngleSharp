namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left
    /// </summary>
    sealed class CssBorderLeftProperty : CssShorthandProperty
    {
        #region ctor

        internal CssBorderLeftProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderLeft, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return CssBorderProperty.Converter.TryConvert(value, m =>
            {
                Get<CssBorderLeftWidthProperty>().TrySetValue(m.Item1);
                Get<CssBorderLeftStyleProperty>().TrySetValue(m.Item2);
                Get<CssBorderLeftColorProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var color = properties.OfType<CssBorderLeftColorProperty>().FirstOrDefault();
            var width = properties.OfType<CssBorderLeftWidthProperty>().FirstOrDefault();
            var style = properties.OfType<CssBorderLeftStyleProperty>().FirstOrDefault();

            if (color == null || width == null || style == null)
                return String.Empty;

            return CssBorderProperty.SerializeValue(width, style, color);
        }

        #endregion
    }
}

namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top
    /// </summary>
    sealed class CssBorderTopProperty : CssShorthandProperty
    {
        #region ctor

        internal CssBorderTopProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderTop, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return CssBorderProperty.Converter.TryConvert(value, m =>
            {
                Get<CssBorderTopWidthProperty>().TrySetValue(m.Item1);
                Get<CssBorderTopStyleProperty>().TrySetValue(m.Item2);
                Get<CssBorderTopColorProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var color = properties.OfType<CssBorderTopColorProperty>().FirstOrDefault();
            var width = properties.OfType<CssBorderTopWidthProperty>().FirstOrDefault();
            var style = properties.OfType<CssBorderTopStyleProperty>().FirstOrDefault();

            if (color == null || width == null || style == null)
                return String.Empty;

            return CssBorderProperty.SerializeValue(width, style, color);
        }

        #endregion
    }
}

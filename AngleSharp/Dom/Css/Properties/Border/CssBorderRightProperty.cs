namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right
    /// </summary>
    sealed class CssBorderRightProperty : CssShorthandProperty
    {
        #region ctor

        internal CssBorderRightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderRight, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return CssBorderProperty.Converter.TryConvert(value, m =>
            {
                Get<CssBorderRightWidthProperty>().TrySetValue(m.Item1);
                Get<CssBorderRightStyleProperty>().TrySetValue(m.Item2);
                Get<CssBorderRightColorProperty>().TrySetValue(m.Item3);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var color = properties.OfType<CssBorderRightColorProperty>().FirstOrDefault();
            var width = properties.OfType<CssBorderRightWidthProperty>().FirstOrDefault();
            var style = properties.OfType<CssBorderRightStyleProperty>().FirstOrDefault();

            if (color == null || width == null || style == null)
                return String.Empty;

            return CssBorderProperty.SerializeValue(width, style, color);
        }

        #endregion
    }
}

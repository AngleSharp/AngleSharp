namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right
    /// </summary>
    sealed class CssBorderRightProperty : CssShorthandProperty
    {
        #region ctor

        internal CssBorderRightProperty()
            : base(PropertyNames.BorderRight, PropertyFlags.Animatable)
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
            return CssBorderProperty.StyleConverter.Validate(value);
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssBorderRightWidthProperty>().TrySetValue(m.Item1);
                Get<CssBorderRightStyleProperty>().TrySetValue(m.Item2);
                Get<CssBorderRightColorProperty>().TrySetValue(m.Item3);
            });*/
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

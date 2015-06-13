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
        #region Fields

        //TODO Convert instead of validate
        /*
            Get<CssBorderRightWidthProperty>().TrySetValue(m.Item1);
            Get<CssBorderRightStyleProperty>().TrySetValue(m.Item2);
            Get<CssBorderRightColorProperty>().TrySetValue(m.Item3);
        */
        static readonly IValueConverter StyleConverter = CssBorderProperty.StyleConverter;

        #endregion

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

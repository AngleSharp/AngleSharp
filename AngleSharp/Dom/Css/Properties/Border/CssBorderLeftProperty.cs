namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left
    /// </summary>
    sealed class CssBorderLeftProperty : CssShorthandProperty
    {
        #region Fields

        //TODO Convert instead of validate
        /*
            Get<CssBorderLeftWidthProperty>().TrySetValue(m.Item1);
            Get<CssBorderLeftStyleProperty>().TrySetValue(m.Item2);
            Get<CssBorderLeftColorProperty>().TrySetValue(m.Item3);
         */
        static readonly IValueConverter StyleConverter = CssBorderProperty.StyleConverter;

        #endregion

        #region ctor

        internal CssBorderLeftProperty()
            : base(PropertyNames.BorderLeft, PropertyFlags.Animatable)
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

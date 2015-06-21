namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    sealed class CssBorderRadiusProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.BorderRadiusShorthandConverter.OrDefault();

        #endregion

        #region ctor

        internal CssBorderRadiusProperty()
            : base(PropertyNames.BorderRadius, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion
    }
}

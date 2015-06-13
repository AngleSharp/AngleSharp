namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-width
    /// </summary>
    sealed class CssBorderWidthProperty : CssShorthandProperty
    {
        #region Fields

        //TODO Convert instead of validate
        /*
            Get<CssBorderTopWidthProperty>().TrySetValue(m.Item1);
            Get<CssBorderRightWidthProperty>().TrySetValue(m.Item2);
            Get<CssBorderBottomWidthProperty>().TrySetValue(m.Item3);
            Get<CssBorderLeftWidthProperty>().TrySetValue(m.Item4);
         */
        static readonly IValueConverter StyleConverter = Converters.LineWidthConverter.Periodic();

        #endregion

        #region ctor

        internal CssBorderWidthProperty()
            : base(PropertyNames.BorderWidth, PropertyFlags.Animatable)
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
            var top = properties.OfType<CssBorderTopWidthProperty>().FirstOrDefault();
            var right = properties.OfType<CssBorderRightWidthProperty>().FirstOrDefault();
            var bottom = properties.OfType<CssBorderBottomWidthProperty>().FirstOrDefault();
            var left = properties.OfType<CssBorderLeftWidthProperty>().FirstOrDefault();

            if (top == null || right == null || bottom == null || left == null)
                return String.Empty;
            else if (!top.HasValue || !right.HasValue || !bottom.HasValue || !left.HasValue)
                return String.Empty;

            return SerializePeriodic(top, right, bottom, left);
        }

        #endregion
    }
}

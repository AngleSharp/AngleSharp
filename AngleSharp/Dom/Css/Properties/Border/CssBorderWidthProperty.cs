namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-width
    /// </summary>
    sealed class CssBorderWidthProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter =
            Converters.LineWidthConverter.Val().Periodic();

        #endregion

        #region ctor

        internal CssBorderWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderWidth, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssBorderTopWidthProperty>().TrySetValue(m.Item1);
                Get<CssBorderRightWidthProperty>().TrySetValue(m.Item2);
                Get<CssBorderBottomWidthProperty>().TrySetValue(m.Item3);
                Get<CssBorderLeftWidthProperty>().TrySetValue(m.Item4);
            });
        }

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

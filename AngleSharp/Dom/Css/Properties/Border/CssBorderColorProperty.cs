namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-color
    /// </summary>
    sealed class CssBorderColorProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter =
            Converters.CurrentColorConverter.Val().Periodic();

        #endregion

        #region ctor

        internal CssBorderColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderColor, rule, PropertyFlags.Hashless | PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssBorderTopColorProperty>().TrySetValue(m.Item1);
                Get<CssBorderRightColorProperty>().TrySetValue(m.Item2);
                Get<CssBorderBottomColorProperty>().TrySetValue(m.Item3);
                Get<CssBorderLeftColorProperty>().TrySetValue(m.Item4);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var top = properties.OfType<CssBorderTopColorProperty>().FirstOrDefault();
            var right = properties.OfType<CssBorderRightColorProperty>().FirstOrDefault();
            var bottom = properties.OfType<CssBorderBottomColorProperty>().FirstOrDefault();
            var left = properties.OfType<CssBorderLeftColorProperty>().FirstOrDefault();

            if (top == null || right == null || bottom == null || left == null)
                return String.Empty;
            else if (!top.HasValue || !right.HasValue || !bottom.HasValue || !left.HasValue)
                return String.Empty;

            return SerializePeriodic(top, right, bottom, left);
        }

        #endregion
    }
}

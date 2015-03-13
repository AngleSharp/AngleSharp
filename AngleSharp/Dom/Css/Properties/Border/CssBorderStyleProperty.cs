namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-style
    /// </summary>
    sealed class CssBorderStyleProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter =
            Converters.LineStyleConverter.Val().Periodic();

        #endregion

        #region ctor

        internal CssBorderStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderStyle, rule)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssBorderTopStyleProperty>().TrySetValue(m.Item1);
                Get<CssBorderRightStyleProperty>().TrySetValue(m.Item2);
                Get<CssBorderBottomStyleProperty>().TrySetValue(m.Item3);
                Get<CssBorderLeftStyleProperty>().TrySetValue(m.Item4);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var top = properties.OfType<CssBorderTopStyleProperty>().FirstOrDefault();
            var right = properties.OfType<CssBorderRightStyleProperty>().FirstOrDefault();
            var bottom = properties.OfType<CssBorderBottomStyleProperty>().FirstOrDefault();
            var left = properties.OfType<CssBorderLeftStyleProperty>().FirstOrDefault();

            if (top == null || right == null || bottom == null || left == null)
                return String.Empty;
            else if (!top.HasValue || !right.HasValue || !bottom.HasValue || !left.HasValue)
                return String.Empty;

            return SerializePeriodic(top, right, bottom, left);
        }

        #endregion
    }
}

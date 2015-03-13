namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CssPaddingProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<ICssValue, ICssValue, ICssValue, ICssValue>> Converter =
            Converters.LengthOrPercentConverter.Val().Periodic();

        #endregion

        #region ctor

        internal CssPaddingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Padding, rule)
        {
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m =>
            {
                Get<CssPaddingTopProperty>().TrySetValue(m.Item1);
                Get<CssPaddingRightProperty>().TrySetValue(m.Item2);
                Get<CssPaddingBottomProperty>().TrySetValue(m.Item3);
                Get<CssPaddingLeftProperty>().TrySetValue(m.Item4);
            });
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var top = properties.OfType<CssPaddingTopProperty>().FirstOrDefault();
            var right = properties.OfType<CssPaddingRightProperty>().FirstOrDefault();
            var bottom = properties.OfType<CssPaddingBottomProperty>().FirstOrDefault();
            var left = properties.OfType<CssPaddingLeftProperty>().FirstOrDefault();

            if (top == null || right == null || bottom == null || left == null)
                return String.Empty;
            else if (!top.HasValue || !right.HasValue || !bottom.HasValue || !left.HasValue)
                return String.Empty;

            return SerializePeriodic(top, right, bottom, left);
        }

        #endregion
    }
}

namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-color
    /// </summary>
    sealed class CssBorderColorProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<CssValue, CssValue, CssValue, CssValue>> StyleConverter =
            Converters.CurrentColorConverter.Val().Periodic();

        #endregion

        #region ctor

        internal CssBorderColorProperty()
            : base(PropertyNames.BorderColor, PropertyFlags.Hashless | PropertyFlags.Animatable)
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

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.TryConvert(value, m =>
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

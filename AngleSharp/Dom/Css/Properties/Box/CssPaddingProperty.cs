namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CssPaddingProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter.Periodic();

        #endregion

        #region ctor

        internal CssPaddingProperty()
            : base(PropertyNames.Padding)
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
            return StyleConverter.Convert(value) != null;
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssPaddingTopProperty>().TrySetValue(m.Item1);
                Get<CssPaddingRightProperty>().TrySetValue(m.Item2);
                Get<CssPaddingBottomProperty>().TrySetValue(m.Item3);
                Get<CssPaddingLeftProperty>().TrySetValue(m.Item4);
            });*/
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

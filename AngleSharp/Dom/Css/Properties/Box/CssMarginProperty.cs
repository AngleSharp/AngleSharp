namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    sealed class CssMarginProperty : CssShorthandProperty
    {
        #region Fields

        //TODO Convert instead of validate
        /*
            Get<CssMarginTopProperty>().TrySetValue(m.Item1);
            Get<CssMarginRightProperty>().TrySetValue(m.Item2);
            Get<CssMarginBottomProperty>().TrySetValue(m.Item3);
            Get<CssMarginLeftProperty>().TrySetValue(m.Item4);
        */
        static readonly IValueConverter StyleConverter = Converters.AutoLengthOrPercentConverter.Periodic();

        #endregion

        #region ctor

        internal CssMarginProperty()
            : base(PropertyNames.Margin)
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
            var top = properties.OfType<CssMarginTopProperty>().FirstOrDefault();
            var right = properties.OfType<CssMarginRightProperty>().FirstOrDefault();
            var bottom = properties.OfType<CssMarginBottomProperty>().FirstOrDefault();
            var left = properties.OfType<CssMarginLeftProperty>().FirstOrDefault();

            if (top == null || right == null || bottom == null || left == null)
                return String.Empty;
            else if (!top.HasValue || !right.HasValue || !bottom.HasValue || !left.HasValue)
                return String.Empty;

            return SerializePeriodic(top, right, bottom, left);
        }

        #endregion
    }
}

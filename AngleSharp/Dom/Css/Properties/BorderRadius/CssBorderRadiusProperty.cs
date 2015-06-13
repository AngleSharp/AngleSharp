namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    sealed class CssBorderRadiusProperty : CssShorthandProperty
    {
        #region Fields

        static readonly IValueConverter RadiusConverter = Converters.WithOrder(
            Converters.LengthOrPercentConverter.Periodic().Required(),
            Converters.LengthOrPercentConverter.Periodic().StartsWithDelimiter().Option());

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
            get { return RadiusConverter; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CssValue value)
        {
            return RadiusConverter.Convert(value) != null;
            //TODO Convert instead of validate
            /*, m =>
            {
                Get<CssBorderTopLeftRadiusProperty>().TrySetValue(Extract(m, 0));
                Get<CssBorderTopRightRadiusProperty>().TrySetValue(Extract(m, 1));
                Get<CssBorderBottomRightRadiusProperty>().TrySetValue(Extract(m, 2));
                Get<CssBorderBottomLeftRadiusProperty>().TrySetValue(Extract(m, 3));
            });*/
        }

        internal override String SerializeValue(IEnumerable<CssProperty> properties)
        {
            var topLeft = properties.OfType<CssBorderTopLeftRadiusProperty>().FirstOrDefault();
            var topRight = properties.OfType<CssBorderTopRightRadiusProperty>().FirstOrDefault();
            var bottomRight = properties.OfType<CssBorderBottomRightRadiusProperty>().FirstOrDefault();
            var bottomLeft = properties.OfType<CssBorderBottomLeftRadiusProperty>().FirstOrDefault();

            if (topLeft == null || topRight == null || bottomRight == null || bottomLeft == null)
                return String.Empty;
            else if (!topLeft.HasValue || !topRight.HasValue || !bottomRight.HasValue || !bottomLeft.HasValue)
                return String.Empty;

            return SerializePeriodic(topLeft, topRight, bottomRight, bottomLeft);
        }

        #endregion

        #region Helper

        static CssValue Extract(Tuple<CssValue, CssValue> src, Int32 index)
        {
            //var hv = src.Item1;
            //var vv = src.Item2;
            //var h = hv as CssValueList;
            //var v = vv as CssValueList;

            //if (h != null)
            //    hv = Find(h, index);

            //if (vv == null)
            //    return hv;

            //var value = new CssValueList();
            //value.Add(hv);

            //if (v != null)
            //    vv = Find(v, index);

            //value.Add(vv);
            //return value;
            return null;
        }

        //static ICssValue Find(CssValue list, Int32 index)
        //{
        //    if (index < list.Count)
        //        return list[index];
        //    else if (index == 3 && 1 < list.Length)
        //        return list[1];
            
        //    return list[0];
        //}

        #endregion
    }
}

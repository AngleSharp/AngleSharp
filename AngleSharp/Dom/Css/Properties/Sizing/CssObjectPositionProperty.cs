namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-position
    /// </summary>
    sealed class CssObjectPositionProperty : CssProperty
    {
        #region ctor

        internal CssObjectPositionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ObjectPosition, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Point.Center;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.PointConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.PointConverter.Validate(value);
        }

        #endregion
    }
}

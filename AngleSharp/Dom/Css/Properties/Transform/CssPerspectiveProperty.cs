namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective
    /// Gets the distance from the user to the z=0 plane. It is used to
    /// apply a perspective transform to the element and its content. If it
    /// 0 or a negative value, no perspective transform is applied.
    /// </summary>
    sealed class CssPerspectiveProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Length> Converter = 
            Converters.LengthConverter.Or(Keywords.None, Length.Zero);

        #endregion

        #region ctor

        internal CssPerspectiveProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Perspective, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

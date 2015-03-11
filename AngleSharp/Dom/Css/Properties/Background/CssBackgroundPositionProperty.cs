namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// Gets the list of all given positions.
    /// </summary>
    sealed class CssBackgroundPositionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Point[]> Converter = 
            Converters.PointConverter.FromList();

        #endregion

        #region ctor

        internal CssBackgroundPositionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackgroundPosition, rule, PropertyFlags.Animatable)
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
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

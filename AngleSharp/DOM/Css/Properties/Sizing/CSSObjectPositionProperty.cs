namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using System;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-position
    /// </summary>
    sealed class CSSObjectPositionProperty : CSSProperty
    {
        #region Fields

        internal static readonly IValueConverter<Point> Converter = Converters.PointConverter;
        internal static readonly Point Default = Point.Center;
        Point _position;

        #endregion

        #region ctor

        internal CSSObjectPositionProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ObjectPosition, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        public Point Position
        {
            get { return _position; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => _position = m);
        }

        internal override void Reset()
        {
            _position = Default;
        }

        #endregion
    }
}

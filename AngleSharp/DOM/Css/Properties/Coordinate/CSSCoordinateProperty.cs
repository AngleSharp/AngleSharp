namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Basis for all properties that have either a length
    /// or percentage value or an auto value - nothing else.
    /// </summary>
    abstract class CssCoordinateProperty : CssProperty
    {
        #region Fields

        internal static readonly Length? Default = null;
        internal static readonly IValueConverter<Length?> Converter = Converters.AutoLengthOrPercentConverter;
        Length? _distance;

        #endregion

        #region ctor

        internal CssCoordinateProperty(String name, CssStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the position is automatically calculated.
        /// </summary>
        public Boolean IsAuto
        {
            get { return _distance == null; }
        }

        /// <summary>
        /// Gets the position if a fixed position has been set.
        /// </summary>
        public Length? Position
        {
            get { return _distance; }
        }

        #endregion

        #region Methods

        public void SetPosition(Length? distance)
        {
            _distance = distance;
        }

        internal override void Reset()
        {
            _distance = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetPosition);
        }

        #endregion
    }
}

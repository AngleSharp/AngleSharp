namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Basis for all properties that have either a length
    /// or percentage value or an auto value - nothing else.
    /// </summary>
    abstract class CSSCoordinateProperty : CSSProperty
    {
        #region Fields

        internal static readonly IDistance Default = null;
        internal static readonly IValueConverter<IDistance> Converter = Converters.AutoDistanceConverter;
        IDistance _distance;

        #endregion

        #region ctor

        internal CSSCoordinateProperty(String name, CSSStyleDeclaration rule)
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
        public IDistance Position
        {
            get { return _distance; }
        }

        #endregion

        #region Methods

        public void SetPosition(IDistance distance)
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

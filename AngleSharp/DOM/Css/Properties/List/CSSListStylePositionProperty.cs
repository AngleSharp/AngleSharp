namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// </summary>
    sealed class CssListStylePositionProperty : CssProperty, ICssListStylePositionProperty
    {
        #region Fields

        internal static readonly ListPosition Default = ListPosition.Outside;
        internal static readonly IValueConverter<ListPosition> Converter = Map.ListPositions.ToConverter();
        ListPosition _position;

        #endregion

        #region ctor

        internal CssListStylePositionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ListStylePosition, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected position.
        /// </summary>
        public ListPosition Position
        {
            get { return _position; }
        }

        #endregion

        #region Methods

        public void SetPosition(ListPosition position)
        {
            _position = position;
        }

        internal override void Reset()
        {
            _position = Default;
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

namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// </summary>
    sealed class CSSListStylePositionProperty : CSSProperty, ICssListStylePositionProperty
    {
        #region Fields

        ListPosition _position;

        #endregion

        #region ctor

        internal CSSListStylePositionProperty(CSSStyleDeclaration rule)
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
            _position = ListPosition.Outside;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithListPosition().TryConvert(value, SetPosition);
        }

        #endregion
    }
}

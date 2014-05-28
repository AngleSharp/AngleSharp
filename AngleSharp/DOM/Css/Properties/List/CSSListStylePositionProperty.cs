namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-position
    /// </summary>
    public sealed class CSSListStylePositionProperty : CSSProperty
    {
        #region Fields

        ListPosition _position;

        #endregion

        #region ctor

        internal CSSListStylePositionProperty()
            : base(PropertyNames.ListStylePosition)
        {
            _inherited = true;
            _position = ListPosition.Outside;
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var position = value.ToListPosition();

            if (position.HasValue)
                _position = position.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

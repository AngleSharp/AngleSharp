namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// The abstract base class for all border-width sub properties.
    /// </summary>
    public abstract class CSSBorderPartWidthProperty : CSSProperty
    {
        #region Fields

        Length _width;

        #endregion

        #region ctor

        internal CSSBorderPartWidthProperty(String name)
            : base(name)
        {
            _inherited = false;
            _width = Length.Medium;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the thickness of the given border.
        /// </summary>
        public Length Width
        {
            get { return _width; }
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
            var length = value.ToBorderWidth();

            if (length.HasValue)
                _width = length.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

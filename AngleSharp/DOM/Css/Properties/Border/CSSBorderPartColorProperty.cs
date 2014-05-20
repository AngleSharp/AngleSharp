namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// The abstract base class for all border-color sub-properties.
    /// </summary>
    public abstract class CSSBorderPartColorProperty : CSSProperty
    {
        #region Fields

        Color _color;

        #endregion

        #region ctor

        internal CSSBorderPartColorProperty(String name)
            : base(name)
        {
            _color = Color.Transparent;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected color for the border.
        /// </summary>
        public Color Color
        {
            get { return _color; }
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
            var color = value.ToColor();

            if (color.HasValue)
                _color = color.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

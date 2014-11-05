namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-width
    /// </summary>
    sealed class CSSOutlineWidthProperty : CSSProperty, ICssOutlineWidthProperty
    {
        #region Fields

        Length _width;

        #endregion

        #region ctor

        internal CSSOutlineWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.OutlineWidth, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the outline of an element. An outline is a
        /// line that is drawn around elements, outside the border edge,
        /// to make the element stand out.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _width = Length.Medium;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var length = value.ToBorderWidth();

            if (length.HasValue)
            {
                _width = length.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}

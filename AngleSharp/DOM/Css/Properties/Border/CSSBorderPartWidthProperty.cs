namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// The abstract base class for all border-width sub properties.
    /// </summary>
    abstract class CSSBorderPartWidthProperty : CSSProperty
    {
        #region Fields

        Length _width;

        #endregion

        #region ctor

        internal CSSBorderPartWidthProperty(String name, CSSStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
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

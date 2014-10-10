namespace AngleSharp.DOM.Css.Properties
{
    using System;
    
    /// <summary>
    /// More information can be found:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clip
    /// </summary>
    sealed class CSSClipProperty : CSSProperty, ICssClipProperty
    {
        #region Fields

        Shape _shape;

        #endregion

        #region ctor

        internal CSSClipProperty()
            : base(PropertyNames.Clip, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the shape of the selected clipping region.
        /// If this value is null, then the clipping is
        /// determined automatically.
        /// </summary>
        public Shape Clip
        {
            get { return _shape; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _shape = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var shape = value.ToShape();

            if (shape != null)
                _shape = shape;
            else if (value.Is(Keywords.Auto))
                _shape = null;
            else
                return false;

            return true;
        }

        #endregion
    }
}

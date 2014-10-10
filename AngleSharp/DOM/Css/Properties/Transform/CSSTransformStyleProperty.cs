namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-style
    /// </summary>
    sealed class CSSTransformStyleProperty : CSSProperty, ICssTransformStyleProperty
    {
        #region Fields

        Boolean _flat;

        #endregion

        #region ctor

        internal CSSTransformStyleProperty()
            : base(PropertyNames.TransformStyle)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the children of the element are lying in the plane of
        /// the element itself. Otherwise the children of the element should
        /// be positioned in the 3D-space.
        /// </summary>
        public Boolean IsFlat
        {
            get { return _flat; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _flat = true;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.Flat))
                _flat = true;
            else if (value.Is(Keywords.Preserve3d))
                _flat = false;
            else
                return false;

            return true;
        }

        #endregion
    }
}

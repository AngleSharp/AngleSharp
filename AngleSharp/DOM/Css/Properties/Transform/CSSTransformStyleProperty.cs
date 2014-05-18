namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-style
    /// </summary>
    public sealed class CSSTransformStyleProperty : CSSProperty
    {
        #region Fields

        Boolean _flat;

        #endregion

        #region ctor

        internal CSSTransformStyleProperty()
            : base(PropertyNames.TransformStyle)
        {
            _flat = true;
            _inherited = false;
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("flat"))
                _flat = true;
            else if (value.Is("preserve-3d"))
                _flat = false;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/z-index
    /// </summary>
    public sealed class CSSZIndexProperty : CSSProperty
    {
        #region Fields

        Int32? _value;

        #endregion

        #region ctor

        internal CSSZIndexProperty()
            : base(PropertyNames.ZIndex)
        {
            _value = null;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the z-index has been set at all.
        /// </summary>
        public Boolean HasIndex
        {
            get { return _value.HasValue; }
        }

        /// <summary>
        /// Gets the index in the stacking order, if any.
        /// </summary>
        public Int32 Index
        {
            get { return _value.HasValue ? _value.Value : 0; }
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
            if (value is CSSPrimitiveValue<Number>)
                _value = (Int32)((CSSPrimitiveValue<Number>)value).Value.Value;
            else if (value.Is("auto"))
                _value = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-spacing
    /// </summary>
    sealed class CSSBorderSpacingProperty : CSSProperty, ICssBorderSpacingProperty
    {
        #region Fields

        Length _h;
        Length _v;

        #endregion

        #region ctor

        internal CSSBorderSpacingProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderSpacing, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal spacing between cells, that is the space
        /// between cells in adjacent columns.
        /// </summary>
        public Length Horizontal
        {
            get { return _h; }
        }

        /// <summary>
        /// Gets the vertical spacing between cells, that is the space
        /// between cells in adjacent rows.
        /// </summary>
        public Length Vertical
        {
            get { return _v; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _h = Length.Zero;
            _v = Length.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
                _h = _v = length.Value;
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length != 2)
                    return false;

                var h = values[0].ToLength();
                var v = values[1].ToLength();

                if (!h.HasValue || !v.HasValue)
                    return false;

                _h = h.Value;
                _v = v.Value;
            }
            else
                return false;

            return true;
        }

        #endregion
    }
}

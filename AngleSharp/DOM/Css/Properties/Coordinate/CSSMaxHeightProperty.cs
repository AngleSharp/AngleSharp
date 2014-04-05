namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-height
    /// </summary>
    sealed class CSSMaxHeightProperty : CSSProperty
    {
        #region Fields

        static readonly NoMaxHeightMode _none = new NoMaxHeightMode();
        MaxHeightMode _mode;

        #endregion

        #region ctor

        public CSSMaxHeightProperty()
            : base(PropertyNames.MaxHeight)
        {
            _inherited = false;
            _mode = _none;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
                _mode = new AbsoluteMaxHeightMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativeMaxHeightMode(((CSSPercentValue)value).Value);
            else if (value is CSSIdentifierValue && (((CSSIdentifierValue)value).Value).Equals("none", StringComparison.OrdinalIgnoreCase))
                _mode = _none;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class MaxHeightMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// No limit on the height of the box.
        /// </summary>
        sealed class NoMaxHeightMode : MaxHeightMode
        {
        }

        /// <summary>
        /// A fixed maximum height.
        /// </summary>
        sealed class AbsoluteMaxHeightMode : MaxHeightMode
        {
            Length _height;
            
            public AbsoluteMaxHeightMode(Length height)
            {
                _height = height;
            }
        }

        /// <summary>
        /// The percentage is calculated with respect to the height of the
        /// containing block. If the height of the containing block is not
        /// specified explicitly, the percentage value is treated as none.
        /// </summary>
        sealed class RelativeMaxHeightMode : MaxHeightMode
        {
            Single _scale;

            public RelativeMaxHeightMode(Single scale)
            {
                _scale = scale;
            }
        }

        #endregion
    }
}

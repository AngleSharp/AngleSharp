namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-width
    /// </summary>
    sealed class CSSMaxWidthProperty : CSSProperty
    {
        #region Fields

        static readonly NoMaxWidthMode _none = new NoMaxWidthMode();
        MaxWidthMode _mode;

        #endregion

        #region ctor

        public CSSMaxWidthProperty()
            : base(PropertyNames.MaxWidth)
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
                _mode = new AbsoluteMaxWidthMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativeMaxWidthMode(((CSSPercentValue)value).Value);
            else if (value is CSSIdentifierValue && (((CSSIdentifierValue)value).Value).Equals("none", StringComparison.OrdinalIgnoreCase))
                _mode = _none;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class MaxWidthMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The width has no maximum value.
        /// </summary>
        sealed class NoMaxWidthMode : MaxWidthMode
        {
        }

        /// <summary>
        /// Specified as an absolute length.
        /// </summary>
        sealed class AbsoluteMaxWidthMode : MaxWidthMode
        {
            Length _height;
            
            public AbsoluteMaxWidthMode(Length height)
            {
                _height = height;
            }
        }

        /// <summary>
        /// Specified as a relative of containing block's width.
        /// </summary>
        sealed class RelativeMaxWidthMode : MaxWidthMode
        {
            Single _scale;

            public RelativeMaxWidthMode(Single scale)
            {
                _scale = scale;
            }
        }

        #endregion
    }
}

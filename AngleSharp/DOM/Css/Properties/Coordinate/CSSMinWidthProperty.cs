namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-width
    /// </summary>
    sealed class CSSMinWidthProperty : CSSProperty
    {
        #region Fields

        static readonly AbsoluteMinWidthMode _default = new AbsoluteMinWidthMode(Length.Zero);
        MinWidthMode _mode;

        #endregion

        #region ctor

        public CSSMinWidthProperty()
            : base(PropertyNames.MinWidth)
        {
            _inherited = false;
            _mode = _default;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
                _mode = new AbsoluteMinWidthMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativeMinWidthMode(((CSSPercentValue)value).Value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class MinWidthMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The fixed minimum width. Negative values make the declaration invalid.
        /// </summary>
        sealed class AbsoluteMinWidthMode : MinWidthMode
        {
            Length _height;
            
            public AbsoluteMinWidthMode(Length height)
            {
                _height = height;
            }
        }

        /// <summary>
        /// The fixed minimum width expressed as a relative of containing block's width.
        /// Negative values make the declaration invalid.
        /// </summary>
        sealed class RelativeMinWidthMode : MinWidthMode
        {
            Single _scale;

            public RelativeMinWidthMode(Single scale)
            {
                _scale = scale;
            }
        }

        #endregion
    }
}

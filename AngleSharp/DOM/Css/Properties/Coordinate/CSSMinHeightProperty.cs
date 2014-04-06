namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-height
    /// </summary>
    sealed class CSSMinHeightProperty : CSSProperty
    {
        #region Fields

        static readonly AbsoluteMinHeightMode _default = new AbsoluteMinHeightMode(Length.Zero);
        MinHeightMode _mode;

        #endregion

        #region ctor

        public CSSMinHeightProperty()
            : base(PropertyNames.MinHeight)
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
                _mode = new AbsoluteMinHeightMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativeMinHeightMode(((CSSPercentValue)value).Value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class MinHeightMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The fixed minimum height. Negative values make the declaration invalid.
        /// </summary>
        sealed class AbsoluteMinHeightMode : MinHeightMode
        {
            Length _height;
            
            public AbsoluteMinHeightMode(Length height)
            {
                _height = height;
            }
        }

        /// <summary>
        /// The fixed minimum height expressed as a relative of containing block's height.
        /// Negative values make the declaration invalid.
        /// </summary>
        sealed class RelativeMinHeightMode : MinHeightMode
        {
            Single _scale;

            public RelativeMinHeightMode(Single scale)
            {
                _scale = scale;
            }
        }

        #endregion
    }
}

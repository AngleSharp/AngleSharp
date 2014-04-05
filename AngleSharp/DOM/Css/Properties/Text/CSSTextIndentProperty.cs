namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-indent
    /// </summary>
    sealed class CSSTextIndentProperty : CSSProperty
    {
        #region Fields

        static readonly AbsoluteIntendMode _default = new AbsoluteIntendMode(Length.Zero);
        IntendMode _mode;

        #endregion

        #region ctor

        public CSSTextIndentProperty()
            : base(PropertyNames.TextIndent)
        {
            _inherited = true;
            _mode = _default;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
                _mode = new AbsoluteIntendMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativeIntendMode(((CSSPercentValue)value).Value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class IntendMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// Indentation is specified as fixed length.
        /// Negative values are allowed.
        /// </summary>
        sealed class AbsoluteIntendMode : IntendMode
        {
            Length _height;

            public AbsoluteIntendMode(Length height)
            {
                _height = height;
            }
        }

        /// <summary>
        /// Indentation is a percentage of the containing block width.
        /// </summary>
        sealed class RelativeIntendMode : IntendMode
        {
            Single _scale;

            public RelativeIntendMode(Single scale)
            {
                _scale = scale;
            }
        }

        #endregion
    }
}

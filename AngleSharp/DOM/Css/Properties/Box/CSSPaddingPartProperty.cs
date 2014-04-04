namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Basis for all elementary padding properties.
    /// </summary>
    class CSSPaddingPartProperty : CSSProperty
    {
        #region Fields

        static readonly AbsolutePaddingMode _default = new AbsolutePaddingMode(Length.Zero);
        PaddingMode _mode;

        #endregion

        #region ctor

        protected CSSPaddingPartProperty(String name)
            : base(name)
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
                _mode = new AbsolutePaddingMode(length.Value);
            else if (value is CSSPercentValue)
                _mode = new RelativePaddingMode(((CSSPercentValue)value).Value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Mode

        abstract class PaddingMode
        {
            //TODO add members
        }

        sealed class RelativePaddingMode : PaddingMode
        {
            Single _percent;

            public RelativePaddingMode(Single percent)
            {
                _percent = percent;
            }
        }

        sealed class AbsolutePaddingMode : PaddingMode
        {
            Length _length;

            public AbsolutePaddingMode(Length length)
            {
                _length = length;
            }
        }

        #endregion
    }
}

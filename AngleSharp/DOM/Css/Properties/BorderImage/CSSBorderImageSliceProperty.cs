namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-slice
    /// </summary>
    public sealed class CSSBorderImageSliceProperty : CSSProperty
    {
        #region Fields

        #endregion

        #region ctor

        internal CSSBorderImageSliceProperty()
            : base(PropertyNames.BorderImageSlice)
        {
            _inherited = false;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

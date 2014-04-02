namespace AngleSharp.DOM.Css.Properties
{
    using System;
    
    /// <summary>
    /// More information can be found:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clip
    /// </summary>
    sealed class CSSClipProperty : CSSProperty
    {
        #region Fields

        static readonly AutoClipMode _auto = new AutoClipMode();
        ClipMode _mode;

        #endregion

        #region ctor

        public CSSClipProperty()
            : base(PropertyNames.CLIP)
        {
            _mode = _auto;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSShapeValue)
            {
                var shape = (CSSShapeValue)value;
                _mode = new CustomClipMode(shape.Top, shape.Right, shape.Bottom, shape.Left);
            }
            else if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("auto", StringComparison.OrdinalIgnoreCase))
                _mode = _auto;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Nested

        abstract class ClipMode
        {
            //TODO Members that make sense
        }

        sealed class AutoClipMode : ClipMode
        { }

        sealed class CustomClipMode : ClipMode
        {
            Length _top;
            Length _right;
            Length _bottom;
            Length _left;

            public CustomClipMode(Length top, Length right, Length bottom, Length left)
            {
                _top = top;
                _right = right;
                _bottom = bottom;
                _left = left;
            }
        }

        #endregion
    }
}

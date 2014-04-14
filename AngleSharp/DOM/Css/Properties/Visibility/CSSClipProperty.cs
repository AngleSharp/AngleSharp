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

        CSSShapeValue _shape;

        #endregion

        #region ctor

        public CSSClipProperty()
            : base(PropertyNames.Clip)
        {
            _shape = null;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var shape = value as CSSShapeValue;

            if (shape != null)
                _shape = shape;
            else if (value.Is("auto"))
                _shape = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/color
    /// </summary>
    sealed class CSSColorProperty : CSSProperty
    {
        #region Fields

        Color _color;

        #endregion

        #region ctor

        public CSSColorProperty()
            : base(PropertyNames.Color)
        {
            _inherited = true;
            _color = Color.Black;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var color = value.ToColor();

            if (color.HasValue)
                _color = color.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

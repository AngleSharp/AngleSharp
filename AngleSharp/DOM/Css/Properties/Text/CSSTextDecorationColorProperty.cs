namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-color
    /// </summary>
    sealed class CSSTextDecorationColorProperty : CSSProperty
    {
        #region Fields

        Color _color;

        #endregion

        #region ctor

        public CSSTextDecorationColorProperty()
            : base(PropertyNames.TextDecorationColor)
        {
            _inherited = false;
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

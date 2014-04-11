namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-image
    /// </summary>
    sealed class CSSBackgroundImageProperty : CSSProperty
    {
        #region Fields

        List<Uri> _images;

        #endregion

        #region ctor

        public CSSBackgroundImageProperty()
            : base(PropertyNames.BackgroundImage)
        {
            _inherited = false;
            _images = new List<Uri>();
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("none", StringComparison.OrdinalIgnoreCase))
                _images.Clear();
            //TODO
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

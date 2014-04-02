namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/height
    /// </summary>
    sealed class CSSHeightProperty : CSSCoordinateProperty
    {
        #region ctor

        public CSSHeightProperty()
            : base(PropertyNames.HEIGHT)
        {
        }

        #endregion
    }
}

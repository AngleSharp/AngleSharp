namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/left
    /// </summary>
    sealed class CSSLeftProperty : CSSCoordinateProperty
    {
        #region ctor

        public CSSLeftProperty()
            : base(PropertyNames.Left)
        {
        }

        #endregion
    }
}

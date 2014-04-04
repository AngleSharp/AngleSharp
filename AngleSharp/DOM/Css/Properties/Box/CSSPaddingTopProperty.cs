namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-top
    /// </summary>
    sealed class CSSPaddingTopProperty : CSSPaddingPartProperty
    {
        #region ctor

        public CSSPaddingTopProperty()
            : base(PropertyNames.PaddingTop)
        {
        }

        #endregion
    }
}

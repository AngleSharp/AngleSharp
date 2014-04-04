namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-left
    /// </summary>
    sealed class CSSPaddingLeftProperty : CSSPaddingPartProperty
    {
        #region ctor

        public CSSPaddingLeftProperty()
            : base(PropertyNames.PaddingLeft)
        {
        }

        #endregion
    }
}

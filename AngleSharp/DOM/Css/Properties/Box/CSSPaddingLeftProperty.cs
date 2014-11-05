namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-left
    /// </summary>
    sealed class CSSPaddingLeftProperty : CSSPaddingPartProperty, ICssPaddingLeftProperty
    {
        #region ctor

        internal CSSPaddingLeftProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.PaddingLeft, rule)
        {
        }

        #endregion

        #region Property

        public IDistance Left
        {
            get { return Padding; }
        }

        #endregion
    }
}

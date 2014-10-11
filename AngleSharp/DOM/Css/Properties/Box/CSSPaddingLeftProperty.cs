namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-left
    /// </summary>
    sealed class CSSPaddingLeftProperty : CSSPaddingPartProperty, ICssPaddingLeftProperty
    {
        #region ctor

        internal CSSPaddingLeftProperty()
            : base(PropertyNames.PaddingLeft)
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

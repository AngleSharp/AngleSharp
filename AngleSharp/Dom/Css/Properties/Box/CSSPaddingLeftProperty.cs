namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-left
    /// </summary>
    sealed class CssPaddingLeftProperty : CssPaddingPartProperty, ICssPaddingLeftProperty
    {
        #region ctor

        internal CssPaddingLeftProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PaddingLeft, rule)
        {
        }

        #endregion

        #region Property

        public Length Left
        {
            get { return Padding; }
        }

        #endregion
    }
}

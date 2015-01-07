namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding-right
    /// </summary>
    sealed class CssPaddingRightProperty : CssPaddingPartProperty, ICssPaddingRightProperty
    {
        #region ctor

        internal CssPaddingRightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.PaddingRight, rule)
        {
        }

        #endregion

        #region Property

        public Length Right
        {
            get { return Padding; }
        }

        #endregion
    }
}

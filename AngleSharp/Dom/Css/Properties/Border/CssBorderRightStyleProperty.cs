namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-style
    /// </summary>
    sealed class CssBorderRightStyleProperty : CssBorderPartStyleProperty
    {
        #region ctor

        internal CssBorderRightStyleProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderRightStyle, rule)
        {
        }

        #endregion
    }
}

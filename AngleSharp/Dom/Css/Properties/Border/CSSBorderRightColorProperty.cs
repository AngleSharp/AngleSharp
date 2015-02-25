namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right-color
    /// </summary>
    sealed class CssBorderRightColorProperty : CssBorderPartColorProperty, ICssBorderColorProperty
    {
        #region ctor

        internal CssBorderRightColorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderRightColor, rule)
        { 
        }

        #endregion
    }
}

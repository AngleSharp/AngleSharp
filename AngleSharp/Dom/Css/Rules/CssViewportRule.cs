namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// Represents the @viewport rule.
    /// </summary>
    sealed class CssViewportRule : CssDeclarationRule
    {
        #region ctor

        /// <summary>
        /// Creates a new @viewport rule.
        /// </summary>
        internal CssViewportRule(CssParser parser)
            : base(CssRuleType.Viewport, RuleNames.ViewPort, parser)
        {
        }

        #endregion
    }
}

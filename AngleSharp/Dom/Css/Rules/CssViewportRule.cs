namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// Represents the @viewport rule.
    /// </summary>
    sealed class CssViewportRule : CssDeclarationRule
    {
        internal CssViewportRule(CssParser parser)
            : base(CssRuleType.Viewport, RuleNames.ViewPort, parser)
        {
        }
    }
}

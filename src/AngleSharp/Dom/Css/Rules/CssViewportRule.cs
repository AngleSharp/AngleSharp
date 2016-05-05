namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the @viewport rule.
    /// </summary>
    sealed class CssViewportRule : CssDeclarationRule
    {
        internal CssViewportRule(CssParser parser)
            : base(CssRuleType.Viewport, RuleNames.ViewPort, parser)
        {
        }

        protected override CssProperty CreateNewProperty(String name)
        {
            return Factory.Properties.CreateViewport(name);
        }
    }
}

namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// Represents the abstract base class for CSS media and CSS supports rules.
    /// </summary>
    abstract class CssConditionRule : CssGroupingRule
    {
        #region ctor

        internal CssConditionRule (CssRuleType type, CssParser parser)
            : base(type, parser)
	    { 
        }

        #endregion

        #region Internal Methods

        internal abstract Boolean IsValid(RenderDevice device);

        #endregion
    }
}

namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents the abstract base class for
    /// CSS media and CSS supports rules.
    /// </summary>
    abstract class CssConditionRule : CssGroupingRule
    {
        #region ctor

        /// <summary>
        /// Constructs a new CSS condition rule.
        /// </summary>
        internal CssConditionRule (CssRuleType type)
            : base(type)
	    { 
        }

        #endregion

        #region Internal Methods

        internal abstract Boolean IsValid(RenderDevice device);

        #endregion
    }
}

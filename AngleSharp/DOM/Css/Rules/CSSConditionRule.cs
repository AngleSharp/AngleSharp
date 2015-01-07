namespace AngleSharp.DOM.Css
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

        internal override void ComputeStyle(PropertyBag style, RenderDevice device, IElement element)
        {
            if (IsValid(device))
                base.ComputeStyle(style, device, element);
        }

        internal abstract Boolean IsValid(RenderDevice device);

        #endregion
    }
}

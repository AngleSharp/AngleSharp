namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Represents the abstract base class for
    /// CSS media and CSS supports rules.
    /// </summary>
    abstract class CSSConditionRule : CSSGroupingRule
    {
        #region ctor

        /// <summary>
        /// Constructs a new CSS condition rule.
        /// </summary>
        internal CSSConditionRule (CssRuleType type)
            : base(type)
	    { 
        }

        #endregion

        #region Internal Methods

        internal override void ComputeStyle(CssPropertyBag style, RenderDevice device, IElement element)
        {
            if (IsValid(device))
                base.ComputeStyle(style, device, element);
        }

        internal abstract Boolean IsValid(RenderDevice device);

        #endregion
    }
}

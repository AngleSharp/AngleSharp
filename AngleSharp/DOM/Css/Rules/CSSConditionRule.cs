namespace AngleSharp.DOM.Css
{
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
        internal CSSConditionRule ()
	    { 
        }

        #endregion

        #region Internal Methods

        internal override void ComputeStyle(CssPropertyBag style, IWindow window, IElement element)
        {
            if (IsValid(window))
                base.ComputeStyle(style, window, element);
        }

        internal abstract Boolean IsValid(IWindow window);

        #endregion
    }
}

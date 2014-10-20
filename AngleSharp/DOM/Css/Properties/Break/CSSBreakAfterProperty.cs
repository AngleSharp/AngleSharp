namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-after
    /// </summary>
    sealed class CSSBreakAfterProperty : CSSBreakProperty, ICssBreakAfterProperty
    {
        #region ctor

        internal CSSBreakAfterProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BreakAfter, rule)
        {
        }

        #endregion
    }
}

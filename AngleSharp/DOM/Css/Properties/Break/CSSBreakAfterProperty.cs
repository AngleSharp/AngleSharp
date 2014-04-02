namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-after
    /// </summary>
    sealed class CSSBreakAfterProperty : CSSBreakProperty
    {
        #region ctor

        public CSSBreakAfterProperty()
            : base(PropertyNames.BREAK_AFTER)
        {
        }

        #endregion
    }
}

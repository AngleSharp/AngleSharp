namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-inside
    /// or even better
    /// http://dev.w3.org/csswg/css-break/#break-inside
    /// </summary>
    sealed class CSSBreakInsideProperty : CSSBreakProperty
    {
        #region ctor

        public CSSBreakInsideProperty()
            : base(PropertyNames.BREAK_INSIDE)
        {
            //Only limited to
            //auto | avoid | avoid-page | avoid-column | avoid-region
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-before
    /// </summary>
    sealed class CSSBreakBeforeProperty : CSSBreakProperty
    {
        #region ctor

        public CSSBreakBeforeProperty()
            : base(PropertyNames.BreakBefore)
        {
        }

        #endregion
    }
}

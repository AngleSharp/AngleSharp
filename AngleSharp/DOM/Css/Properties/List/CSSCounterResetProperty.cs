namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-reset
    /// </summary>
    sealed class CSSCounterResetProperty : CSSProperty
    {
        #region ctor

        public CSSCounterResetProperty()
            : base(PropertyNames.CounterReset)
        {
            _inherited = false;
        }

        #endregion
    }
}

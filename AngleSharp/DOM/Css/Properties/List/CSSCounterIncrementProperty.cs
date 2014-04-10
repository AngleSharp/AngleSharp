namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-increment
    /// </summary>
    sealed class CSSCounterIncrementProperty : CSSProperty
    {
        #region ctor

        public CSSCounterIncrementProperty()
            : base(PropertyNames.CounterIncrement)
        {
            _inherited = false;
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-timing-function
    /// </summary>
    public sealed class CSSTransitionTimingFunctionProperty : CSSProperty
    {
        #region ctor

        internal CSSTransitionTimingFunctionProperty()
            : base(PropertyNames.TransitionTimingFunction)
        {
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            return base.IsValid(value);
        }

        #endregion
    }
}

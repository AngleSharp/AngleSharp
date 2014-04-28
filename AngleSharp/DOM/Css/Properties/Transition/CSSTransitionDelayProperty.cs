namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-delay
    /// </summary>
    public sealed class CSSTransitionDelayProperty : CSSProperty
    {
        #region ctor

        internal CSSTransitionDelayProperty()
            : base(PropertyNames.TransitionDelay)
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

namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-duration
    /// </summary>
    public sealed class CSSTransitionDurationProperty : CSSProperty
    {
        #region ctor

        internal CSSTransitionDurationProperty()
            : base(PropertyNames.TransitionDuration)
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

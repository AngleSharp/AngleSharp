namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-property
    /// </summary>
    public sealed class CSSTransitionPropertyProperty : CSSProperty
    {
        #region ctor

        internal CSSTransitionPropertyProperty()
            : base(PropertyNames.TransitionProperty)
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

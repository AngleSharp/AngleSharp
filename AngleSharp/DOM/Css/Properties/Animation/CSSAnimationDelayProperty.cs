namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-delay
    /// </summary>
    public sealed class CSSAnimationDelayProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationDelayProperty()
            : base(PropertyNames.AnimationDelay)
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

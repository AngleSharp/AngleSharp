namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-timing-function
    /// </summary>
    public sealed class CSSAnimationTimingFunctionProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationTimingFunctionProperty()
            : base(PropertyNames.AnimationTimingFunction)
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

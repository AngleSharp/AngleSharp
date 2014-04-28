namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-duration
    /// </summary>
    public sealed class CSSAnimationDurationProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationDurationProperty()
            : base(PropertyNames.AnimationDuration)
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

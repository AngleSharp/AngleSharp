namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-play-state
    /// </summary>
    public sealed class CSSAnimationPlayStateProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationPlayStateProperty()
            : base(PropertyNames.AnimationPlayState)
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

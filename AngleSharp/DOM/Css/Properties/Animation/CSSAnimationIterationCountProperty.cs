namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-iteration-count
    /// </summary>
    public sealed class CSSAnimationIterationCountProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationIterationCountProperty()
            : base(PropertyNames.AnimationIterationCount)
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

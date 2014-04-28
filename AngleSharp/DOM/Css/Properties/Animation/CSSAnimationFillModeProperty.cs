namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// </summary>
    public sealed class CSSAnimationFillModeProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationFillModeProperty()
            : base(PropertyNames.AnimationFillMode)
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

namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// </summary>
    public sealed class CSSAnimationDirectionProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationDirectionProperty()
            : base(PropertyNames.AnimationDirection)
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

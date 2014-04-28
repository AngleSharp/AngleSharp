namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-name
    /// </summary>
    public sealed class CSSAnimationNameProperty : CSSProperty
    {
        #region ctor

        internal CSSAnimationNameProperty()
            : base(PropertyNames.AnimationName)
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

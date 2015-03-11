namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/backface-visibility
    /// Gets if the back face is visible, allowing the front face to be
    /// displayed mirrored. Otherwise the back face is not visible, hiding
    /// the front face.
    /// </summary>
    sealed class CssBackfaceVisibilityProperty : CssProperty
    {
        #region ctor

        internal CssBackfaceVisibilityProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BackfaceVisibility, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return true;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.BackfaceVisibilityConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.BackfaceVisibilityConverter.Validate(value);
        }

        #endregion
    }
}

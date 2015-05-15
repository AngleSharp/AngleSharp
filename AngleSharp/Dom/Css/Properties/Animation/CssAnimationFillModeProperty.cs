namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-fill-mode
    /// Gets an iteration over all defined fill modes.
    /// </summary>
    sealed class CssAnimationFillModeProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<AnimationFillStyle[]> Converter = 
            Converters.AnimationFillStyleConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationFillModeProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationFillMode, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return AnimationFillStyle.None;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

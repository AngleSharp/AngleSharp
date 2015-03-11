namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-iteration-count
    /// Gets the iteration count of the covered animations.
    /// </summary>
    sealed class CssAnimationIterationCountProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Single[]> Converter = 
            Converters.PositiveOrInfiniteNumberConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationIterationCountProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationIterationCount, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return 1f;
        }

        protected override Object Compute(IElement element)
        {
            return Converter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

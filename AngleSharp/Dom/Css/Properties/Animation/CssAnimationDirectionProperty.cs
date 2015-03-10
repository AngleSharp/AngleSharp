namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// Gets an iteration over all defined directions.
    /// </summary>
    sealed class CssAnimationDirectionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<AnimationDirection[]> Converter = 
            Converters.AnimationDirectionConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationDirectionProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationDirection, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return AnimationDirection.Normal;
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

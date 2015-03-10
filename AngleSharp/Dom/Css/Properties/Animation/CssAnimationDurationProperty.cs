namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-duration
    /// Gets the durations for the animations.
    /// </summary>
    sealed class CssAnimationDurationProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Time[]> Converter = 
            Converters.TimeConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationDurationProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationDuration, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Time.Zero;
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

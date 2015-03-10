namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-delay
    /// Gets the delays for the animations.
    /// </summary>
    sealed class CssAnimationDelayProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Time[]> Converter = 
            Converters.TimeConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationDelayProperty(CssStyleDeclaration rule)
            : base(PropertyNames.AnimationDelay, rule)
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

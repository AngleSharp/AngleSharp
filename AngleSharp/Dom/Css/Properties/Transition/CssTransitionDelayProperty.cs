namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-delay
    /// Gets the delays for the transitions.
    /// </summary>
    sealed class CssTransitionDelayProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Time[]> Converter =
            Converters.TimeConverter.FromList();

        #endregion

        #region ctor

        internal CssTransitionDelayProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TransitionDelay, rule)
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

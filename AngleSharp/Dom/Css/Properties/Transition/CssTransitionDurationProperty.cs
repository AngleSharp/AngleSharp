namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-duration
    /// Gets the durations for the transitions.
    /// </summary>
    sealed class CssTransitionDurationProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Time[]> Converter =
            Converters.TimeConverter.FromList();

        #endregion

        #region ctor

        internal CssTransitionDurationProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TransitionDuration, rule)
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

        protected override Boolean IsValid(CssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

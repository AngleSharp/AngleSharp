namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-property
    /// Gets the names of the selected properties.
    /// </summary>
    sealed class CssTransitionPropertyProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<String[]> Converter =
            Converters.AnimatableConverter.FromList().Or(Keywords.None, new String[0]);
        
        #endregion

        #region ctor

        internal CssTransitionPropertyProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TransitionProperty, rule)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Keywords.All;
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

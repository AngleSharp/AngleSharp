namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-timing-function
    /// Gets the enumeration over all timing functions.
    /// </summary>
    sealed class CssTransitionTimingFunctionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<ITimingFunction[]> ListConverter =
            Converters.TransitionConverter.FromList();

        #endregion

        #region ctor

        internal CssTransitionTimingFunctionProperty()
            : base(PropertyNames.TransitionTimingFunction)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return ListConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Map.TimingFunctions[Keywords.Ease];
        }

        protected override Object Compute(IElement element)
        {
            return ListConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return ListConverter.Validate(value);
        }

        #endregion
    }
}

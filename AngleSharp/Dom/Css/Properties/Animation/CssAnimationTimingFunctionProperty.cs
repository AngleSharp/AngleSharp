namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-timing-function
    /// Gets the enumeration over all timing functions.
    /// </summary>
    sealed class CssAnimationTimingFunctionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<ITimingFunction[]> ListConverter = 
            Converters.TransitionConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationTimingFunctionProperty()
            : base(PropertyNames.AnimationTimingFunction)
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

        #endregion
    }
}

namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-iteration-count
    /// Gets the iteration count of the covered animations.
    /// </summary>
    sealed class CssAnimationIterationCountProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Single[]> ListConverter = 
            Converters.PositiveOrInfiniteNumberConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationIterationCountProperty()
            : base(PropertyNames.AnimationIterationCount)
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
            return 1f;
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

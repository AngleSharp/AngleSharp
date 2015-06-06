namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-play-state
    /// Gets an enumerable over the defined play states.
    /// </summary>
    sealed class CssAnimationPlayStateProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<PlayState[]> Converter = 
            Converters.PlayStateConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationPlayStateProperty()
            : base(PropertyNames.AnimationPlayState)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return PlayState.Running;
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

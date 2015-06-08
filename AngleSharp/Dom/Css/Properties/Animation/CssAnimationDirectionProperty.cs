namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-direction
    /// Gets an iteration over all defined directions.
    /// </summary>
    sealed class CssAnimationDirectionProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<AnimationDirection[]> ListConverter = 
            Converters.AnimationDirectionConverter.FromList();

        #endregion

        #region ctor

        internal CssAnimationDirectionProperty()
            : base(PropertyNames.AnimationDirection)
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
            return AnimationDirection.Normal;
        }

        protected override Boolean IsValid(CssValue value)
        {
            return ListConverter.Validate(value);
        }

        #endregion
    }
}

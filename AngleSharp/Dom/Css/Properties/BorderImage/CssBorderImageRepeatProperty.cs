namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-repeat
    /// </summary>
    sealed class CssBorderImageRepeatProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<BorderRepeat[]> StyleConverter = 
            Map.BorderRepeatModes.ToConverter().Many(1, 2);

        #endregion

        #region ctor

        internal CssBorderImageRepeatProperty()
            : base(PropertyNames.BorderImageRepeat)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return StyleConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BorderRepeat.Stretch;
        }

        protected override Object Compute(IElement element)
        {
            return StyleConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return StyleConverter.Validate(value);
        }

        #endregion
    }
}

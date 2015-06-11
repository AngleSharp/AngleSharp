namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-spacing
    /// </summary>
    sealed class CssBorderSpacingProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Length[]> SpacingConverter = 
            Converters.LengthConverter.Many(1, 2);

        #endregion

        #region ctor

        internal CssBorderSpacingProperty()
            : base(PropertyNames.BorderSpacing, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return SpacingConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Length.Zero;
        }

        #endregion
    }
}

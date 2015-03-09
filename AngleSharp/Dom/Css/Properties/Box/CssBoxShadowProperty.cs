namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// Gets an enumeration over all the set shadows.
    /// </summary>
    sealed class CssBoxShadowProperty : CssProperty
    {
        #region Fields

        static readonly Shadow[] Default = new Shadow[0];
        internal static readonly IValueConverter<Shadow[]> Converter = 
            Converters.ShadowConverter.FromList().Or(Keywords.None, Default);

        #endregion

        #region ctor

        internal CssBoxShadowProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BoxShadow, rule, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Default;
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

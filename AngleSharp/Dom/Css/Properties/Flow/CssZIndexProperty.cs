namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/z-index
    /// Gets the index in the stacking order, if any.
    /// </summary>
    sealed class CssZIndexProperty : CssProperty
    {
        #region ctor

        internal CssZIndexProperty()
            : base(PropertyNames.ZIndex, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.OptionalIntegerConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return null;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.OptionalIntegerConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.OptionalIntegerConverter.Validate(value);
        }

        #endregion
    }
}

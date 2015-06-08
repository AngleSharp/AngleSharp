namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// Gets the value of the floating property.
    /// </summary>
    sealed class CssFloatProperty : CssProperty
    {
        #region ctor

        internal CssFloatProperty()
            : base(PropertyNames.Float)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.FloatingConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return Floating.None;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.FloatingConverter.Convert(Value);
        }

        protected override Boolean IsValid(CssValue value)
        {
            return Converters.FloatingConverter.Validate(value);
        }

        #endregion
    }
}

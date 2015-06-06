namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform
    /// Gets the enumeration over all transformations.
    /// </summary>
    sealed class CssTransformProperty : CssProperty
    {
        #region Fields

        static readonly ITransform[] Default = new ITransform[0];
        static readonly IValueConverter<ITransform[]> Converter = 
            Converters.TransformConverter.Many().Or(Keywords.None, Default);

        #endregion

        #region ctor

        internal CssTransformProperty()
            : base(PropertyNames.Transform, PropertyFlags.Animatable)
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

        protected override Boolean IsValid(CssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion
    }
}

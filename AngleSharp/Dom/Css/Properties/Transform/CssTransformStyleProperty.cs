namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-style
    /// Gets if the children of the element are lying in the plane of the
    /// element itself. Otherwise the children of the element should be
    /// positioned in the 3D-space.
    /// </summary>
    sealed class CssTransformStyleProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Boolean> Converter = 
            Converters.Toggle(Keywords.Flat, Keywords.Preserve3d);

        #endregion

        #region ctor

        internal CssTransformStyleProperty()
            : base(PropertyNames.TransformStyle)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return true;
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

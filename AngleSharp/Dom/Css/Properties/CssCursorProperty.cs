namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    sealed class CssCursorProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<Tuple<CustomCursor[], SystemCursor>> Converter = 
            Converters.ImageSourceConverter.To(m => new CustomCursor { Image = m }).Or(
                Converters.WithOrder(
                    Converters.ImageSourceConverter.Required(),
                    Converters.NumberConverter.Required(),
                    Converters.NumberConverter.Required()).To(
                v => new CustomCursor { Image = v.Item1, X = v.Item2, Y = v.Item3 })).
                FromList().RequiresEnd(Map.Cursors.ToConverter());

        #endregion

        #region ctor

        internal CssCursorProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Cursor, rule, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return SystemCursor.Auto;
        }

        protected override Object Compute(IElement element)
        {
            var cursors = Converter.Convert(Value);
            return cursors.Item2;
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.Validate(value);
        }

        #endregion

        #region Custom Cursor

        /// <summary>
        /// A url pointing to an image file.
        /// </summary>
        struct CustomCursor
        {
            public IImageSource Image;
            public Single X;
            public Single Y;
        }

        #endregion
    }
}

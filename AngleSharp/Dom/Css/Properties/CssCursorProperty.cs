namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    sealed class CssCursorProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = 
            Converters.ImageSourceConverter.Or(
                Converters.WithOrder(
                    Converters.ImageSourceConverter.Required(),
                    Converters.NumberConverter.Required(),
                    Converters.NumberConverter.Required())).
                FromList().RequiresEnd(Map.Cursors.ToConverter());

        #endregion

        #region ctor

        internal CssCursorProperty()
            : base(PropertyNames.Cursor, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: SystemCursor.Auto
            get { return StyleConverter; }
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

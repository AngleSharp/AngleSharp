namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-fit
    /// </summary>
    sealed class CssObjectFitProperty : CssProperty
    {
        #region ctor

        internal CssObjectFitProperty()
            : base(PropertyNames.ObjectFit)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            // Default: ObjectFitting.Fill
            get { return Converters.ObjectFittingConverter; }
        }

        #endregion
    }
}

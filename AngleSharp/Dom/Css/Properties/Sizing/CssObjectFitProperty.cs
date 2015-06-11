namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

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
            get { return Converters.ObjectFittingConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return ObjectFitting.Fill;
        }

        #endregion
    }
}

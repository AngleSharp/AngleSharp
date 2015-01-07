namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// http://dev.w3.org/csswg/css-images-3/#the-object-fit
    /// </summary>
    sealed class CssObjectFitProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<ObjectFitting> Converter = Map.ObjectFittings.ToConverter();
        internal static readonly ObjectFitting Default = ObjectFitting.Fill;
        ObjectFitting _fitting;

        #endregion

        #region ctor

        internal CssObjectFitProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ObjectFit, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        public ObjectFitting Fitting
        {
            get { return _fitting; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => _fitting = m);
        }

        internal override void Reset()
        {
            _fitting = Default;
        }

        #endregion
    }
}

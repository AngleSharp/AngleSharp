namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-before
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssPageBreakBeforeProperty : CssProperty
    {
        #region ctor

        internal CssPageBreakBeforeProperty()
            : base(PropertyNames.PageBreakBefore)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return Converters.PageBreakModeConverter; }
        }

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return BreakMode.Auto;
        }

        #endregion
    }
}

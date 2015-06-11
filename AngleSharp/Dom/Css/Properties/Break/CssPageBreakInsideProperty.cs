namespace AngleSharp.Dom.Css
{
    using System;
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-inside
    /// Gets the selected break mode.
    /// </summary>
    sealed class CssPageBreakInsideProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter<BreakMode> BreakConverter = 
            Converters.Assign(Keywords.Auto, BreakMode.Auto).Or(Keywords.Avoid, BreakMode.Avoid);

        #endregion

        #region ctor

        internal CssPageBreakInsideProperty()
            : base(PropertyNames.PageBreakInside)
        {
        }

        #endregion

        #region Properties

        internal override IValueConverter Converter
        {
            get { return BreakConverter; }
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

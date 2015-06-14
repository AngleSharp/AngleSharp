namespace AngleSharp.Dom.Css
{
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

        static readonly IValueConverter StyleConverter = Converters.Assign(Keywords.Auto, BreakMode.Auto).Or(Keywords.Avoid, BreakMode.Avoid).OrDefault(BreakMode.Auto);

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
            get { return StyleConverter; }
        }

        #endregion
    }
}

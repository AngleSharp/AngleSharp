namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/orphans
    /// Gets the minimum number of lines in a block container
    /// that must be left at the bottom of the page. 
    /// </summary>
    sealed class CssOrphansProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.NaturalIntegerConverter.OrDefault(2);

        #endregion

        #region ctor

        internal CssOrphansProperty()
            : base(PropertyNames.Orphans, PropertyFlags.Inherited)
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

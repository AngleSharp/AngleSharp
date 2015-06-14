namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/table-layout
    /// Gets if table and column widths are set by the widths of table and
    /// col elements or by the width of the first row of cells. Cells in
    /// subsequent rows do not affect column widths. Otherwise an automatic
    /// table layout algorithm is commonly used by most browsers for table
    /// layout. The width of the table and its cells depends on the content
    /// thereof.
    /// </summary>
    sealed class CssTableLayoutProperty : CssProperty
    {
        #region Fields

        static readonly IValueConverter StyleConverter = Converters.TableLayoutConverter.OrDefault(false);

        #endregion

        #region ctor

        internal CssTableLayoutProperty()
            : base(PropertyNames.TableLayout)
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

namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

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
        #region ctor

        internal CssTableLayoutProperty(CssStyleDeclaration rule)
            : base(PropertyNames.TableLayout, rule)
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        protected override Object GetDefault(IElement element)
        {
            return false;
        }

        protected override Object Compute(IElement element)
        {
            return Converters.TableLayoutConverter.Convert(Value);
        }

        protected override Boolean IsValid(ICssValue value)
        {
            return Converters.TableLayoutConverter.Validate(value);
        }

        #endregion
    }
}

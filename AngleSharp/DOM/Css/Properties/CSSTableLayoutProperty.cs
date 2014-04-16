namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/table-layout
    /// </summary>
    sealed class CSSTableLayoutProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, TableLayout> modes = new Dictionary<String, TableLayout>(StringComparer.OrdinalIgnoreCase);
        TableLayout _mode;

        #endregion

        #region ctor

        static CSSTableLayoutProperty()
        {
            modes.Add("auto", TableLayout.Auto);
            modes.Add("fixed", TableLayout.Fixed);
        }

        public CSSTableLayoutProperty()
            : base(PropertyNames.TableLayout)
        {
            _mode = TableLayout.Auto;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            TableLayout mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        enum TableLayout
        {
            /// <summary>
            /// An automatic table layout algorithm is commonly used by
            /// most browsers for table layout. The width of the table
            /// and its cells depends on the content thereof.
            /// </summary>
            Auto,
            /// <summary>
            /// Table and column widths are set by the widths of table and
            /// col elements or by the width of the first row of cells. Cells
            /// in subsequent rows do not affect column widths.
            /// </summary>
            Fixed
        }

        #endregion
    }
}

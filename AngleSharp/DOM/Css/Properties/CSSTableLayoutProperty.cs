namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/table-layout
    /// </summary>
    public sealed class CSSTableLayoutProperty : CSSProperty
    {
        #region Fields

        Boolean _fixed;

        #endregion

        #region ctor

        internal CSSTableLayoutProperty()
            : base(PropertyNames.TableLayout)
        {
            _fixed = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if table and column widths are set by the widths of table and
        /// col elements or by the width of the first row of cells. Cells in
        /// subsequent rows do not affect column widths. Otherwise an automatic
        /// table layout algorithm is commonly used by most browsers for table
        /// layout. The width of the table and its cells depends on the content
        /// thereof.
        /// </summary>
        public Boolean IsFixed
        {
            get { return _fixed; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("fixed"))
                _fixed = true;
            else if (value.Is("auto"))
                _fixed = false;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

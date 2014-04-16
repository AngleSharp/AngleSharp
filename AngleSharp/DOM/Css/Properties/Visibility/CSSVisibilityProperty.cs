namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// </summary>
    sealed class CSSVisibilityProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, Visibility> modes = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase);
        Visibility _mode;

        #endregion

        #region ctor

        static CSSVisibilityProperty()
        {
            modes.Add("visible", Visibility.Visible);
            modes.Add("hidden", Visibility.Hidden);
            modes.Add("collapse", Visibility.Collapse);
        }

        public CSSVisibilityProperty()
            : base(PropertyNames.Visibility)
        {
            _mode = Visibility.Visible;
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            Visibility mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        enum Visibility
        {
            /// <summary>
            /// Default value, the box is visible.
            /// </summary>
            Visible,
            /// <summary>
            /// The box is invisible (fully transparent, nothing is drawn),
            /// but still affects layout. 
            /// </summary>
            Hidden,
            /// <summary>
            /// For table rows, columns, column groups, and row groups the
            /// row(s) or column(s) are hidden and the space they would have
            /// occupied is removed (as if display: none were applied to the
            /// column/row of the table). However, the size of other rows and
            /// columns is still calculated as though the cells in the collapsed
            /// row(s) or column(s) are present. This was designed for fast
            /// removal of a row/column from a table without having to recalculate
            /// widths and heights for every portion of the table. For XUL elements,
            /// the computed size of the element is always zero, regardless of other
            /// styles that would normally affect the size, although margins still
            /// take effect. For other elements, collapse is treated the same as hidden.
            /// </summary>
            Collapse
        }

        #endregion
    }
}

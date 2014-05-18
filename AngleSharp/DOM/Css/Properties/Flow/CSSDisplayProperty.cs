namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// </summary>
    public sealed class CSSDisplayProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, DisplayMode> modes = new Dictionary<String, DisplayMode>(StringComparer.OrdinalIgnoreCase);
        DisplayMode _mode;

        #endregion

        #region ctor

        static CSSDisplayProperty()
        {
            modes.Add("none", new NoneDisplayMode());
            modes.Add("inline", new InlineDisplayMode());
            modes.Add("block", new BlockDisplayMode());
            modes.Add("inline-block", new InlineBlockDisplayMode());
            modes.Add("list-item", new ListItemDisplayMode());
            modes.Add("inline-table", new InlineTableDisplayMode());
            modes.Add("table", new TableDisplayMode());
            modes.Add("table-cell", new TableCellDisplayMode());
            modes.Add("table-column", new TableColumnDisplayMode());
            modes.Add("table-column-group", new TableColumnGroupDisplayMode());
            modes.Add("table-footer-group", new TableFooterGroupDisplayMode());
            modes.Add("table-header-group", new TableHeaderGroupDisplayMode());
            modes.Add("table-row", new TableRowDisplayMode());
            modes.Add("table-row-group", new TableRowGroupDisplayMode());
            modes.Add("flex", new FlexDisplayMode());
            modes.Add("inline-flex", new InlineFlexDisplayMode());
            modes.Add("grid", new GridDisplayMode());
            modes.Add("inline-grid", new InlineGridDisplayMode());
            modes.Add("run-in", new RunInDisplayMode());
        }

        internal CSSDisplayProperty()
            : base(PropertyNames.Display)
        {
            _mode = modes["inline"];
            _inherited = false;
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
            DisplayMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class DisplayMode
        {
            //TODO Add members that make sense
        }

        class NoneDisplayMode : DisplayMode
        {
        }

        class InlineDisplayMode : DisplayMode
        {
        }

        class BlockDisplayMode : DisplayMode
        {
        }

        class ListItemDisplayMode : DisplayMode
        {
        }

        class InlineBlockDisplayMode : DisplayMode
        {
        }

        class InlineTableDisplayMode : DisplayMode
        {
        }

        class TableDisplayMode : DisplayMode
        {
        }

        class TableCellDisplayMode : DisplayMode
        {
        }

        class TableColumnDisplayMode : DisplayMode
        {
        }

        class TableColumnGroupDisplayMode : DisplayMode
        {
        }

        class TableFooterGroupDisplayMode : DisplayMode
        {
        }

        class TableHeaderGroupDisplayMode : DisplayMode
        {
        }

        class TableRowDisplayMode : DisplayMode
        {
        }

        class TableRowGroupDisplayMode : DisplayMode
        {
        }

        class FlexDisplayMode : DisplayMode
        {
        }

        class InlineFlexDisplayMode : DisplayMode
        {
        }

        class GridDisplayMode : DisplayMode
        {
        }

        class InlineGridDisplayMode : DisplayMode
        {
        }

        class RunInDisplayMode : DisplayMode
        {
        }

        #endregion
    }
}

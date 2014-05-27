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
            modes.Add("none", DisplayMode.None);
            modes.Add("inline", DisplayMode.Inline);
            modes.Add("block", DisplayMode.Block);
            modes.Add("inline-block", DisplayMode.InlineBlock);
            modes.Add("list-item", DisplayMode.ListItem);
            modes.Add("inline-table", DisplayMode.InlineTable);
            modes.Add("table", DisplayMode.Table);
            modes.Add("table-caption", DisplayMode.TableCaption);
            modes.Add("table-cell", DisplayMode.TableCell);
            modes.Add("table-column", DisplayMode.TableColumn);
            modes.Add("table-column-group", DisplayMode.TableColumnGroup);
            modes.Add("table-footer-group", DisplayMode.TableFooterGroup);
            modes.Add("table-header-group", DisplayMode.TableHeaderGroup);
            modes.Add("table-row", DisplayMode.TableRow);
            modes.Add("table-row-group", DisplayMode.TableRowGroup);
            modes.Add("flex", DisplayMode.Flex);
            modes.Add("inline-flex", DisplayMode.InlineFlex);
            modes.Add("grid", DisplayMode.Grid);
            modes.Add("inline-grid", DisplayMode.InlineGrid);
        }

        internal CSSDisplayProperty()
            : base(PropertyNames.Display)
        {
            _mode = DisplayMode.Inline;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the display mode.
        /// </summary>
        public DisplayMode State
        {
            get { return _mode; }
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
    }
}

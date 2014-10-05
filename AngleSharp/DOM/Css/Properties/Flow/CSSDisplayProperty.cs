namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/display
    /// </summary>
    sealed class CSSDisplayProperty : CSSProperty, ICssDisplayProperty
    {
        #region Fields

        static readonly Dictionary<String, DisplayMode> modes = new Dictionary<String, DisplayMode>(StringComparer.OrdinalIgnoreCase);
        DisplayMode _mode;

        #endregion

        #region ctor

        static CSSDisplayProperty()
        {
            modes.Add(Keywords.None, DisplayMode.None);
            modes.Add(Keywords.Inline, DisplayMode.Inline);
            modes.Add(Keywords.Block, DisplayMode.Block);
            modes.Add(Keywords.InlineBlock, DisplayMode.InlineBlock);
            modes.Add(Keywords.ListItem, DisplayMode.ListItem);
            modes.Add(Keywords.InlineTable, DisplayMode.InlineTable);
            modes.Add(Keywords.Table, DisplayMode.Table);
            modes.Add(Keywords.TableCaption, DisplayMode.TableCaption);
            modes.Add(Keywords.TableCell, DisplayMode.TableCell);
            modes.Add(Keywords.TableColumn, DisplayMode.TableColumn);
            modes.Add(Keywords.TableColumnGroup, DisplayMode.TableColumnGroup);
            modes.Add(Keywords.TableFooterGroup, DisplayMode.TableFooterGroup);
            modes.Add(Keywords.TableHeaderGroup, DisplayMode.TableHeaderGroup);
            modes.Add(Keywords.TableRow, DisplayMode.TableRow);
            modes.Add(Keywords.TableRowGroup, DisplayMode.TableRowGroup);
            modes.Add(Keywords.Flex, DisplayMode.Flex);
            modes.Add(Keywords.InlineFlex, DisplayMode.InlineFlex);
            modes.Add(Keywords.Grid, DisplayMode.Grid);
            modes.Add(Keywords.InlineGrid, DisplayMode.InlineGrid);
        }

        internal CSSDisplayProperty()
            : base(PropertyNames.Display)
        {
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

        protected override void Reset()
        {
            _mode = DisplayMode.Inline;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            DisplayMode mode;

            if (modes.TryGetValue(value, out mode))
            {
                _mode = mode;
                return true;
            }

            return false;
        }

        #endregion
    }
}

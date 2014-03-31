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

        static readonly Dictionary<String, TableLayoutMode> modes = new Dictionary<String, TableLayoutMode>(StringComparer.OrdinalIgnoreCase);
        TableLayoutMode _mode;

        #endregion

        #region ctor

        static CSSTableLayoutProperty()
        {
            modes.Add("auto", new AutoTableLayoutMode());
            modes.Add("fixed", new FixedTableLayoutMode());
        }

        public CSSTableLayoutProperty()
            : base(PropertyNames.TABLE_LAYOUT)
        {
            _mode = modes["auto"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                TableLayoutMode mode;

                if (modes.TryGetValue(ident.Value, out mode))
                {
                    _mode = mode;
                    return true;
                }
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion

        #region Modes
        
        abstract class TableLayoutMode
        {
            //TODO Add members that make sense
        }

        class AutoTableLayoutMode : TableLayoutMode
        {
        }

        class FixedTableLayoutMode : TableLayoutMode
        {
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/empty-cells
    /// </summary>
    sealed class CSSEmptyCellsProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, CellMode> modes = new Dictionary<String, CellMode>(StringComparer.OrdinalIgnoreCase);
        CellMode _mode;

        #endregion

        #region ctor

        static CSSEmptyCellsProperty()
        {
            modes.Add("show", new ShowCellMode());
            modes.Add("hide", new HideCellMode());
        }

        public CSSEmptyCellsProperty()
            : base(PropertyNames.EMPTY_CELLS)
        {
            _mode = modes["show"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override CSSValue CheckValue(CSSValue value)
        {
            if (value is CSSIdent)
            {
                var ident = (CSSIdent)value;
                CellMode mode;

                if (modes.TryGetValue(ident.Token, out mode))
                {
                    _mode = mode;
                    return value;
                }
            }
            else if (value == CSSValue.Inherit)
                return value;

            return null;
        }

        #endregion

        #region Modes

        abstract class CellMode
        {
            //TODO Add members that make sense
        }

        class ShowCellMode : CellMode
        {
        }

        class HideCellMode : CellMode
        {
        }

        #endregion
    }
}

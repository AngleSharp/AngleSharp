namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-collapse
    /// </summary>
    sealed class CSSBorderCollapseProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BorderCollapseMode> modes = new Dictionary<String, BorderCollapseMode>(StringComparer.OrdinalIgnoreCase);
        BorderCollapseMode _mode;

        #endregion

        #region ctor

        static CSSBorderCollapseProperty()
        {
            modes.Add("collapse", new CollapseBorderCollapseMode());
            modes.Add("separate", new SeparateBorderCollapseMode());
        }

        public CSSBorderCollapseProperty()
            : base(PropertyNames.BORDER_COLLAPSE)
        {
            _mode = modes["separate"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override CSSValue CheckValue(CSSValue value)
        {
            if (value is CSSIdent)
            {
                var ident = (CSSIdent)value;
                BorderCollapseMode mode;

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
        
        abstract class BorderCollapseMode
        {
            //TODO Add members that make sense
        }

        class CollapseBorderCollapseMode : BorderCollapseMode
        {
        }

        class SeparateBorderCollapseMode : BorderCollapseMode
        {
        }

        #endregion
    }
}

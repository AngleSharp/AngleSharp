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

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                BorderCollapseMode mode;

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
        
        abstract class BorderCollapseMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// Requests the use of the collapsed-border table rendering model.
        /// </summary>
        sealed class CollapseBorderCollapseMode : BorderCollapseMode
        {
        }

        /// <summary>
        /// Requests the use of the separated-border table rendering model. It is the default value.
        /// </summary>
        sealed class SeparateBorderCollapseMode : BorderCollapseMode
        {
        }

        #endregion
    }
}

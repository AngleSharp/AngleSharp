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
            : base(PropertyNames.BorderCollapse)
        {
            _mode = modes["separate"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            BorderCollapseMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
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

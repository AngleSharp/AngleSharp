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

        static readonly Dictionary<String, BorderCollapse> modes = new Dictionary<String, BorderCollapse>(StringComparer.OrdinalIgnoreCase);
        BorderCollapse _mode;

        #endregion

        #region ctor

        static CSSBorderCollapseProperty()
        {
            modes.Add("collapse", BorderCollapse.Collapse);
            modes.Add("separate", BorderCollapse.Separate);
        }

        public CSSBorderCollapseProperty()
            : base(PropertyNames.BorderCollapse)
        {
            _mode = BorderCollapse.Separate;
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            BorderCollapse mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        enum BorderCollapse
        {
            /// <summary>
            /// Requests the use of the collapsed-border table rendering model.
            /// </summary>
            Collapse,
            /// <summary>
            /// Requests the use of the separated-border table rendering model. It is the default value.
            /// </summary>
            Separate
        }

        #endregion
    }
}

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

        static readonly Dictionary<String, VisibilityMode> modes = new Dictionary<String, VisibilityMode>(StringComparer.OrdinalIgnoreCase);
        VisibilityMode _mode;

        #endregion

        #region ctor

        static CSSVisibilityProperty()
        {
            modes.Add("visible", new VisibleVisibilityMode());
            modes.Add("hidden", new HiddenVisibilityMode());
            modes.Add("collapse", new CollapseVisibilityMode());
        }

        public CSSVisibilityProperty()
            : base(PropertyNames.Visibility)
        {
            _mode = modes["visible"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            VisibilityMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class VisibilityMode
        {
            //TODO Add members that make sense
        }

        class VisibleVisibilityMode : VisibilityMode
        {
        }

        class HiddenVisibilityMode : VisibilityMode
        {
        }

        class CollapseVisibilityMode : VisibilityMode
        {
        }

        #endregion
    }
}

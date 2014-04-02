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
            : base(PropertyNames.VISIBILITY)
        {
            _mode = modes["visible"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                VisibilityMode mode;

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

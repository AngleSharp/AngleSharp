namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/backface-visibility
    /// </summary>
    sealed class CSSBackfaceVisibility : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BackfaceVisibilityMode> modes = new Dictionary<String, BackfaceVisibilityMode>(StringComparer.OrdinalIgnoreCase);
        BackfaceVisibilityMode _mode;

        #endregion

        #region ctor

        static CSSBackfaceVisibility()
        {
            modes.Add("visible", new VisibleBackfaceVisibilityMode());
            modes.Add("hidden", new HiddenBackfaceVisibilityMode());
        }

        public CSSBackfaceVisibility()
            : base(PropertyNames.BackfaceVisibility)
        {
            _mode = modes["visible"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                BackfaceVisibilityMode mode;

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

        abstract class BackfaceVisibilityMode
        {
            //TODO Add members that make sense
        }

        class VisibleBackfaceVisibilityMode : BackfaceVisibilityMode
        {
        }

        class HiddenBackfaceVisibilityMode : BackfaceVisibilityMode
        {
        }

        #endregion
    }
}

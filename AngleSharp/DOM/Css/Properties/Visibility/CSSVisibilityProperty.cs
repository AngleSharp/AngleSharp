namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/visibility
    /// </summary>
    public sealed class CSSVisibilityProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, Visibility> modes = new Dictionary<String, Visibility>(StringComparer.OrdinalIgnoreCase);
        Visibility _mode;

        #endregion

        #region ctor

        static CSSVisibilityProperty()
        {
            modes.Add("visible", Visibility.Visible);
            modes.Add("hidden", Visibility.Hidden);
            modes.Add("collapse", Visibility.Collapse);
        }

        internal CSSVisibilityProperty()
            : base(PropertyNames.Visibility)
        {
            _mode = Visibility.Visible;
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            Visibility mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the visibility mode.
        /// </summary>
        public Visibility Visibility
        {
            get { return _mode; }
        }

        #endregion
    }
}

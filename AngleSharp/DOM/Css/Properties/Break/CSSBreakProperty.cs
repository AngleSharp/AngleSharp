namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Basis for break-before or break-after property.
    /// </summary>
    public abstract class CSSBreakProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakMode> modes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        BreakMode _mode;

        #endregion

        #region ctor

        static CSSBreakProperty()
        {
            modes.Add("auto", BreakMode.Auto);
            modes.Add("always", BreakMode.Always);
            modes.Add("avoid", BreakMode.Avoid);
            modes.Add("left", BreakMode.Left);
            modes.Add("right", BreakMode.Right);
            modes.Add("page", BreakMode.Page);
            modes.Add("column", BreakMode.Column);
            modes.Add("avoid-page", BreakMode.AvoidPage);
            modes.Add("avoid-column", BreakMode.AvoidColumn);
        }

        internal CSSBreakProperty(String name)
            : base(name)
        {
            _mode = BreakMode.Auto;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        public BreakMode Mode
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            BreakMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

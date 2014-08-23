namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Basis for page-break-before or page-break-after property.
    /// </summary>
    public abstract class CSSPageBreakProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakMode> modes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        BreakMode _mode;

        #endregion

        #region ctor

        static CSSPageBreakProperty()
        {
            modes.Add("auto", BreakMode.Auto);
            modes.Add("always", BreakMode.Always);
            modes.Add("avoid", BreakMode.Avoid);
            modes.Add("left", BreakMode.Left);
            modes.Add("right", BreakMode.Right);
        }

        internal CSSPageBreakProperty(String name)
            : base(name)
        {
            _mode = BreakMode.Auto;
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
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                BreakMode mode;

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
    }
}

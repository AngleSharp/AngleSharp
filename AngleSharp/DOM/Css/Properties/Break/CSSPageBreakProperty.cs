namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Basis for page-break-before or page-break-after property.
    /// </summary>
    abstract class CSSPageBreakProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakMode> modes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        BreakMode _mode;

        #endregion

        #region ctor

        static CSSPageBreakProperty()
        {
            modes.Add(Keywords.Auto, BreakMode.Auto);
            modes.Add(Keywords.Always, BreakMode.Always);
            modes.Add(Keywords.Avoid, BreakMode.Avoid);
            modes.Add(Keywords.Left, BreakMode.Left);
            modes.Add(Keywords.Right, BreakMode.Right);
        }

        internal CSSPageBreakProperty(String name)
            : base(name)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        public BreakMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = BreakMode.Auto;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            BreakMode mode;

            if (modes.TryGetValue(value, out mode))
            {
                _mode = mode;
                return true;
            }

            return false;
        }

        #endregion
    }
}

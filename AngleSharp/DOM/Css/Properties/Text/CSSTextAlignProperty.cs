namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-align
    /// </summary>
    public sealed class CSSTextAlignProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, HorizontalAlignment> modes = new Dictionary<String, HorizontalAlignment>(StringComparer.OrdinalIgnoreCase);
        HorizontalAlignment _mode;

        #endregion

        #region ctor

        static CSSTextAlignProperty()
        {
            modes.Add("left", HorizontalAlignment.Left);
            modes.Add("right", HorizontalAlignment.Right);
            modes.Add("center", HorizontalAlignment.Center);
            modes.Add("justify", HorizontalAlignment.Justify);
        }

        internal CSSTextAlignProperty()
            : base(PropertyNames.TextAlign)
        {
            _mode = HorizontalAlignment.Left;
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected horizontal alignment mode.
        /// </summary>
        public HorizontalAlignment Align
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
            HorizontalAlignment mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

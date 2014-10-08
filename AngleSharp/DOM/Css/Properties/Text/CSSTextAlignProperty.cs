namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-align
    /// </summary>
    sealed class CSSTextAlignProperty : CSSProperty, ICssTextAlignProperty
    {
        #region Fields

        static readonly Dictionary<String, HorizontalAlignment> modes = new Dictionary<String, HorizontalAlignment>(StringComparer.OrdinalIgnoreCase);
        HorizontalAlignment _mode;

        #endregion

        #region ctor

        static CSSTextAlignProperty()
        {
            modes.Add(Keywords.Left, HorizontalAlignment.Left);
            modes.Add(Keywords.Right, HorizontalAlignment.Right);
            modes.Add(Keywords.Center, HorizontalAlignment.Center);
            modes.Add(Keywords.Justify, HorizontalAlignment.Justify);
        }

        internal CSSTextAlignProperty()
            : base(PropertyNames.TextAlign, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected horizontal alignment mode.
        /// </summary>
        public HorizontalAlignment State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            _mode = HorizontalAlignment.Left;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            HorizontalAlignment mode;

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

namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align
    /// </summary>
    sealed class CSSVerticalAlignProperty : CSSProperty, ICssVerticalAlignProperty
    {
        #region Fields

        static readonly Dictionary<String, VerticalAlignment> modes = new Dictionary<String, VerticalAlignment>(StringComparer.OrdinalIgnoreCase);
        VerticalAlignment _mode;
        IDistance _shift;

        #endregion

        #region ctor

        static CSSVerticalAlignProperty()
        {
            modes.Add(Keywords.Baseline, VerticalAlignment.Baseline);
            modes.Add(Keywords.Sub, VerticalAlignment.Sub);
            modes.Add(Keywords.Super, VerticalAlignment.Super);
            modes.Add(Keywords.TextTop, VerticalAlignment.TextTop);
            modes.Add(Keywords.TextBottom, VerticalAlignment.TextBottom);
            modes.Add(Keywords.Middle, VerticalAlignment.Middle);
            modes.Add(Keywords.Top, VerticalAlignment.Top);
            modes.Add(Keywords.Bottom, VerticalAlignment.Bottom);
        }

        internal CSSVerticalAlignProperty()
            : base(PropertyNames.VerticalAlign)
        {
            _mode = VerticalAlignment.Baseline;
            _shift = Percent.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the alignment of of the element's baseline at the given length above
        /// the baseline of its parent or like absolute values, with the percentage
        /// being a percent of the line-height property.
        /// </summary>
        public IDistance Shift
        {
            get { return _shift; }
        }

        /// <summary>
        /// Gets the selected vertical alignment mode.
        /// </summary>
        public VerticalAlignment State
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
            VerticalAlignment mode;
            var distance = value.ToDistance();

            if (distance != null)
            {
                _shift = distance;
                _mode = VerticalAlignment.Baseline;
            }
            else if (modes.TryGetValue(value, out mode))
            {
                _shift = Percent.Zero;
                _mode = mode;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

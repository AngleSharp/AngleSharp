namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    sealed class CSSBorderImageWidthProperty : CSSProperty, ICssBorderImageWidthProperty
    {
        #region Fields

        internal static readonly IDistance Default = Percent.Hundred;
        internal static readonly IValueConverter<Tuple<IDistance, IDistance, IDistance, IDistance>> Converter = WithImageBorderWidth().Periodic();
        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;

        #endregion

        #region ctor

        internal CSSBorderImageWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageWidth, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the top length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the height of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthTop
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the bottom length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the height of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthBottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the left length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the width of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthLeft
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets the right length of the image slice. It can be an absolute or
        /// relative length. This length must not be negative. If a percentage of
        /// the image slice is given it is relative to the width of the border
        /// image area. The percentage must not be negative.
        /// </summary>
        public IDistance WidthRight
        {
            get { return _right; }
        }

        #endregion

        #region Methods

        void SetWidth(IDistance top, IDistance right, IDistance bottom, IDistance left)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
        }

        internal override void Reset()
        {
            _top = Default;
            _right = Default;
            _bottom = Default;
            _left = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => SetWidth(m.Item1, m.Item2, m.Item3, m.Item4));
        }

        #endregion
    }
}

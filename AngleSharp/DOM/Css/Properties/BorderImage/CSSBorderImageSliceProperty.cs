namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-slice
    /// or even better:
    /// http://dev.w3.org/csswg/css-backgrounds/#border-image-slice
    /// </summary>
    sealed class CSSBorderImageSliceProperty : CSSProperty, ICssBorderImageSliceProperty
    {
        #region Fields

        internal static readonly Length Default = new Length(100f, Length.Unit.Percent);
        internal static readonly IValueConverter<Tuple<Length, Length?, Length?, Length?, Boolean>> Converter = Converters.WithAny(
            Converters.BorderSliceConverter.Option(Default),
            Converters.BorderSliceConverter.ToNullable().Option(null),
            Converters.BorderSliceConverter.ToNullable().Option(null),
            Converters.BorderSliceConverter.ToNullable().Option(null),
            Converters.Assign(Keywords.Fill, true).Option(false)
        );

        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;
        Boolean _fill;

        #endregion

        #region ctor

        internal CSSBorderImageSliceProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageSlice, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the top slicing line.
        /// </summary>
        public IDistance SliceTop
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the position of the right slicing line.
        /// </summary>
        public IDistance SliceRight
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the position of the bottom slicing line.
        /// </summary>
        public IDistance SliceBottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the position of the left slicing line.
        /// </summary>
        public IDistance SliceLeft
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets if the center patch should be filled.
        /// </summary>
        public Boolean IsFilled
        {
            get { return _fill; }
        }

        #endregion

        #region Methods

        void SetSlice(IDistance top, IDistance right, IDistance bottom, IDistance left, Boolean fill = false)
        {
            _top = top;
            _right = right ?? _top;
            _bottom = bottom ?? _top;
            _left = left ?? _right;
            _fill = fill;
        }

        internal override void Reset()
        {
            _top = Default;
            _right = Default;
            _bottom = Default;
            _left = Default;
            _fill = false;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => SetSlice(m.Item1, m.Item2, m.Item3, m.Item4, m.Item5));
        }

        #endregion
    }
}

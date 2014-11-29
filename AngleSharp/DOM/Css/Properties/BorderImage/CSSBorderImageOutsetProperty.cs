namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-outset
    /// </summary>
    sealed class CSSBorderImageOutsetProperty : CSSProperty, ICssBorderImageOutsetProperty
    {
        #region Fields

        internal static readonly IDistance Default = Percent.Zero;
        internal static readonly IValueConverter<Tuple<IDistance, IDistance, IDistance, IDistance>> Converter = WithDistance().Periodic();
        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;

        #endregion

        #region ctor

        internal CSSBorderImageOutsetProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageOutset, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length or percentage for the outset of the top border.
        /// </summary>
        public IDistance OutsetTop
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the right border.
        /// </summary>
        public IDistance OutsetRight
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the bottom border.
        /// </summary>
        public IDistance OutsetBottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the left border.
        /// </summary>
        public IDistance OutsetLeft
        {
            get { return _left; }
        }

        #endregion

        #region Methods

        public void SetOutset(IDistance top, IDistance right, IDistance bottom, IDistance left)
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
        protected override Boolean IsValid(CSSValue value)
        {
            return Converter.TryConvert(value, m => SetOutset(m.Item1, m.Item2, m.Item3, m.Item4));
        }

        #endregion
    }
}

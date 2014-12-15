namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-size
    /// </summary>
    sealed class CSSBackgroundSizeProperty : CSSProperty, ICssBackgroundSizeProperty
    {
        #region Fields

        internal static readonly BackgroundSize Default = new BackgroundSize();
        internal static readonly IValueConverter<BackgroundSize> SingleConverter = Converters.AutoLengthOrPercentConverter.To(m => new BackgroundSize { Width = m ?? Length.Full, Height = Length.Full }).Or(
            Keywords.Cover, new BackgroundSize { IsCovered = true }).Or(
            Keywords.Contain, new BackgroundSize { IsContained = true }).Or(
            Converters.WithOrder(Converters.AutoLengthOrPercentConverter.Required(), Converters.AutoLengthOrPercentConverter.Required()).To(pt => new BackgroundSize { Width = pt.Item1 ?? Length.Full, Height = pt.Item2 ?? Length.Full }));
        internal static readonly IValueConverter<BackgroundSize[]> Converter = SingleConverter.FromList();
        readonly List<BackgroundSize> _sizes;

        #endregion

        #region ctor

        internal CSSBackgroundSizeProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundSize, rule, PropertyFlags.Animatable)
        {
            _sizes = new List<BackgroundSize>();
            Reset();
        }

        #endregion

        #region Properties

        public IEnumerable<Boolean> IsCovered
        {
            get { return _sizes.Select(m => m.IsCovered); }
        }

        public IEnumerable<Boolean> IsContained
        {
            get { return _sizes.Select(m => m.IsContained); }
        }

        public IEnumerable<Point> Sizes
        {
            get { return _sizes.Select(m => new Point(m.Width, m.Height)); }
        }

        #endregion

        #region Methods

        public void SetSizes(IEnumerable<BackgroundSize> sizes)
        {
            _sizes.Clear();
            _sizes.AddRange(sizes);
        }

        internal override void Reset()
        {
            _sizes.Clear();
            _sizes.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetSizes);
        }

        #endregion

        #region Structure

        internal struct BackgroundSize
        {
            public Boolean IsCovered;
            public Boolean IsContained;
            public Length Width;
            public Length Height;
        }

        #endregion
    }
}

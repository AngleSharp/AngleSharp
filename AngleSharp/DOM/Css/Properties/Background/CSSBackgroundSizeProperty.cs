namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
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

        internal static readonly BackgroundSize Cover = new BackgroundSize { IsCovered = true };
        internal static readonly BackgroundSize Contain = new BackgroundSize { IsContained = true };
        internal static readonly BackgroundSize Default = new BackgroundSize();
        internal static readonly IValueConverter<BackgroundSize> SingleConverter = Converters.AutoDistanceConverter.To(m => new BackgroundSize { Width = m }).Or(
            Keywords.Cover, Cover).Or(
            Keywords.Contain, Contain).Or(
            Converters.WithOrder(
                Converters.AutoDistanceConverter.Required(),
                Converters.AutoDistanceConverter.Required()).To(
            pt => new BackgroundSize { Width = pt.Item1, Height = pt.Item2 }));
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
    }
}

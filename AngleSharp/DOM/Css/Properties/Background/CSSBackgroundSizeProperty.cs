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

        static readonly SizeMode Auto = new SizeMode { };
        static readonly SizeMode Cover = new SizeMode { IsCovered = true };
        static readonly SizeMode Contain = new SizeMode { IsContained = true };
        readonly List<SizeMode> _sizes;

        #endregion

        #region ctor

        internal CSSBackgroundSizeProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundSize, rule, PropertyFlags.Animatable)
        {
            _sizes = new List<SizeMode>();
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

        private void SetSizes(IEnumerable<SizeMode> sizes)
        {
            _sizes.Clear();
            _sizes.AddRange(sizes);
        }

        internal override void Reset()
        {
            _sizes.Clear();
            _sizes.Add(new SizeMode());
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.TakeList(
                    this.WithDistance().To(m => new SizeMode { Width = m }).Or(
                    this.TakeOne(Keywords.Auto, Auto)).Or(
                    this.TakeOne(Keywords.Cover, Cover)).Or(
                    this.TakeOne(Keywords.Contain, Contain)).Or(
                    this.WithArgs(this.WithDistance(), this.WithDistance(), pt => new SizeMode { Width = pt.Item1, Height = pt.Item2 }))
                ).TryConvert(value, SetSizes);
        }

        #endregion

        #region Modes

        struct SizeMode
        {
            public Boolean IsCovered;
            public Boolean IsContained;
            public IDistance Width;
            public IDistance Height;
        }

        #endregion
    }
}

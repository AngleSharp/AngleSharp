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
            if (value is CSSValueList)
                return CheckList((CSSValueList)value);
            else if (CheckSingle(value))
                return true;

            return false;
        }

        static SizeMode? Check(CSSValue value)
        {
            var distance = value.ToDistance();

            if (distance != null)
                return new SizeMode { Width = distance };
            else if (value.Is(Keywords.Auto))
                return new SizeMode { };
            else if (value.Is(Keywords.Cover))
                return new SizeMode { IsCovered = true };
            else if (value.Is(Keywords.Contain))
                return new SizeMode { IsContained = true };

            return null;
        }

        static SizeMode? Check(CSSValue horizontal, CSSValue vertical)
        {
            var width = horizontal.ToDistance();
            var height = vertical.ToDistance();

            if (width == null && !horizontal.Is(Keywords.Auto))
                return null;
            else if (height == null && !vertical.Is(Keywords.Auto))
                return null;

            return new SizeMode { Width = width, Height = height };
        }

        Boolean CheckSingle(CSSValue value)
        {
            var size = Check(value);

            if (size == null)
                return false;

            _sizes.Clear();
            _sizes.Add(size.Value);
            return true;
        }

        Boolean CheckList(CSSValueList values)
        {
            var sizes = new List<SizeMode>();
            var list = values.ToList();

            foreach (var entry in list)
            {
                while (entry.Length == 0 || entry.Length > 2)
                    return false;

                var size = entry.Length == 1 ? Check(entry[0]) : Check(entry[0], entry[1]);

                if (size == null)
                    return false;

                sizes.Add(size.Value);
            }

            _sizes.Clear();
            _sizes.AddRange(sizes);
            return true;
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

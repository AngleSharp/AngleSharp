namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// </summary>
    sealed class CSSTransformOriginProperty : CSSProperty, ICssTransformOriginProperty
    {
        #region Fields

        IDistance _x;
        IDistance _y;
        IDistance _z;

        #endregion

        #region ctor

        internal CSSTransformOriginProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TransformOrigin, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets how far from the left edge of the box the origin of the transform is set.
        /// </summary>
        public IDistance X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets how far from the top edge of the box the origin of the transform is set.
        /// </summary>
        public IDistance Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets how far from the user eye the z = 0 origin is set.
        /// </summary>
        public IDistance Z
        {
            get { return _z; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _x = Percent.Fifty;
            _y = Percent.Fifty;
            _z = Percent.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSValueList)
                return SetXYZ((CSSValueList)value);

            return SetSingle(value);
        }

        Boolean SetXYZ(CSSValueList list)
        {
            IDistance z = null;

            if (list.Length == 3)
            {
                if (!list[2].ToLength().HasValue)
                    return false;

                z = list[2].ToDistance();
            }

            if (z != null || list.Length == 2)
            {
                var x = GetMode(list[0], Keywords.Left, Keywords.Right);
                var y = GetMode(list[1], Keywords.Top, Keywords.Bottom);

                if (y == null || x == null)
                {
                    x = GetMode(list[1], Keywords.Left, Keywords.Right);
                    y = GetMode(list[0], Keywords.Top, Keywords.Bottom);
                }

                if (x != null && y != null)
                {
                    _x = x;
                    _y = y;
                    _z = z ?? Percent.Zero;
                    return true;
                }
            }

            return false;
        }

        Boolean SetSingle(CSSValue value)
        {
            var calc = value.ToDistance();

            if (calc != null)
            {
                _x = calc;
                _y = calc;
                return true;
            }

            var ident = value.ToIdentifier();

            if (ident != null)
            {
                if (ident.Equals(Keywords.Left, StringComparison.OrdinalIgnoreCase))
                {
                    _x = Percent.Zero;
                    _y = Percent.Fifty;
                    _z = Percent.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Center, StringComparison.OrdinalIgnoreCase))
                {
                    _x = Percent.Fifty;
                    _y = Percent.Fifty;
                    _z = Percent.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Right, StringComparison.OrdinalIgnoreCase))
                {
                    _x = Percent.Hundred;
                    _y = Percent.Fifty;
                    _z = Percent.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Top, StringComparison.OrdinalIgnoreCase))
                {
                    _x = Percent.Fifty;
                    _y = Percent.Zero;
                    _z = Percent.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Bottom, StringComparison.OrdinalIgnoreCase))
                {
                    _x = Percent.Fifty;
                    _y = Percent.Hundred;
                    _z = Percent.Zero;
                    return true;
                }
            }

            return false;
        }

        static IDistance GetMode(CSSValue value, String minIdentifier, String maxIdentifier)
        {
            var calc = value.ToDistance();

            if (calc == null)
            {
                var ident = value.ToIdentifier();

                if (ident != null)
                {
                    if (ident.Equals(minIdentifier, StringComparison.OrdinalIgnoreCase))
                        calc = Percent.Zero;
                    else if (ident.Equals(maxIdentifier, StringComparison.OrdinalIgnoreCase))
                        calc = Percent.Hundred;
                    else if (ident.Equals(Keywords.Center, StringComparison.OrdinalIgnoreCase))
                        calc = Percent.Fifty;
                }
            }

            return calc;
        }

        #endregion
    }
}

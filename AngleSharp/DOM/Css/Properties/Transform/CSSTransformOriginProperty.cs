namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-origin
    /// </summary>
    sealed class CSSTransformOriginProperty : CSSProperty, ICssTransformOriginProperty
    {
        #region Fields

        CSSCalcValue _x;
        CSSCalcValue _y;
        CSSCalcValue _z;

        #endregion

        #region ctor

        internal CSSTransformOriginProperty()
            : base(PropertyNames.TransformOrigin)
        {
            _x = CSSCalcValue.Center;
            _y = CSSCalcValue.Center;
            _z = CSSCalcValue.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets how far from the left edge of the box the origin of the transform is set.
        /// </summary>
        internal CSSCalcValue X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets how far from the top edge of the box the origin of the transform is set.
        /// </summary>
        internal CSSCalcValue Y
        {
            get { return _y; }
        }

        /// <summary>
        /// Gets how far from the user eye the z = 0 origin is set.
        /// </summary>
        internal CSSCalcValue Z
        {
            get { return _z; }
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
            if (value == CSSValue.Inherit)
                return true;
            else if (value is CSSValueList)
                return SetXYZ((CSSValueList)value);

            return SetSingle(value);
        }

        Boolean SetXYZ(CSSValueList list)
        {
            var z = CSSCalcValue.Zero;

            if (list.Length == 3)
            {
                if (!list[2].ToLength().HasValue)
                    return false;

                z = list[2].AsCalc();
            }

            if (z != CSSCalcValue.Zero || list.Length == 2)
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
                    _z = z;
                    return true;
                }
            }

            return false;
        }

        Boolean SetSingle(CSSValue value)
        {
            var calc = value.AsCalc();

            if (calc != null)
            {
                _x = calc;
                _y = calc;
                return true;
            }
            else if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                if (ident.Equals(Keywords.Left, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Zero;
                    _y = CSSCalcValue.Center;
                    _z = CSSCalcValue.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Center, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Center;
                    _y = CSSCalcValue.Center;
                    _z = CSSCalcValue.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Right, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Full;
                    _y = CSSCalcValue.Center;
                    _z = CSSCalcValue.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Top, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Center;
                    _y = CSSCalcValue.Zero;
                    _z = CSSCalcValue.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Bottom, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Center;
                    _y = CSSCalcValue.Full;
                    _z = CSSCalcValue.Zero;
                    return true;
                }
            }

            return false;
        }

        static CSSCalcValue GetMode(CSSValue value, String minIdentifier, String maxIdentifier)
        {
            var calc = value.AsCalc();

            if (calc == null && value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                if (ident.Equals(minIdentifier, StringComparison.OrdinalIgnoreCase))
                    calc = CSSCalcValue.Zero;
                else if (ident.Equals(maxIdentifier, StringComparison.OrdinalIgnoreCase))
                    calc = CSSCalcValue.Full;
                else if (ident.Equals(Keywords.Center, StringComparison.OrdinalIgnoreCase))
                    calc = CSSCalcValue.Center;
            }

            return calc;
        }

        #endregion
    }
}

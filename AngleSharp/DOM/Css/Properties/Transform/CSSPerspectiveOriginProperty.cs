namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// </summary>
    sealed class CSSPerspectiveOriginProperty : CSSProperty, ICssPerspectiveOriginProperty
    {
        #region Fields

        CSSCalcValue _x;
        CSSCalcValue _y;

        #endregion

        #region ctor

        internal CSSPerspectiveOriginProperty()
            : base(PropertyNames.PerspectiveOrigin)
        {
            _x = CSSCalcValue.Center;
            _y = CSSCalcValue.Center;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the abscissa of the vanishing point.
        /// </summary>
        internal CSSCalcValue X
        {
            get { return _x; }
        }

        /// <summary>
        /// Gets the position of the ordinate of the vanishing point.
        /// </summary>
        internal CSSCalcValue Y
        {
            get { return _y; }
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
                return SetXY((CSSValueList)value);
            
            return SetSingle(value);
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

            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Ident)
            {
                var ident = primitive.GetString();

                if (ident.Equals(Keywords.Left, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Zero;
                    _y = CSSCalcValue.Center;
                    return true;
                }
                else if (ident.Equals(Keywords.Right, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Full;
                    _y = CSSCalcValue.Center;
                    return true;
                }
                else if (ident.Equals(Keywords.Center, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Center;
                    _y = CSSCalcValue.Center;
                    return true;
                }
                else if (ident.Equals(Keywords.Top, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Center;
                    _y = CSSCalcValue.Zero;
                    return true;
                }
                else if (ident.Equals(Keywords.Bottom, StringComparison.OrdinalIgnoreCase))
                {
                    _x = CSSCalcValue.Center;
                    _y = CSSCalcValue.Full;
                    return true;
                }
            }

            return false;
        }

        Boolean SetXY(CSSValueList list)
        {
            if (list.Length == 2)
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
                    return true;
                }
            }

            return false;
        }

        static CSSCalcValue GetMode(CSSValue value, String minIdentifier, String maxIdentifier)
        {
            var calc = value.AsCalc();

            if (calc == null && value is CSSPrimitiveValue)
            {
                var primitive = (CSSPrimitiveValue)value;

                if (primitive.Unit == UnitType.Ident)
                {
                    var ident = primitive.GetString();

                    if (ident.Equals(minIdentifier, StringComparison.OrdinalIgnoreCase))
                        calc = CSSCalcValue.Zero;
                    else if (ident.Equals(maxIdentifier, StringComparison.OrdinalIgnoreCase))
                        calc = CSSCalcValue.Full;
                    else if (ident.Equals(Keywords.Center, StringComparison.OrdinalIgnoreCase))
                        calc = CSSCalcValue.Center;
                }
            }

            return calc;
        }

        #endregion
    }
}

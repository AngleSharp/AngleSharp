namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// </summary>
    public sealed class CSSBackgroundPositionProperty : CSSProperty
    {
        #region Fields

        List<CSSPointValue> _positions;

        #endregion

        #region ctor

        internal CSSBackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition)
        {
            _inherited = false;
            _positions = new List<CSSPointValue>();
            _positions.Add(CSSPointValue.Centered);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of all given positions.
        /// </summary>
        public IEnumerable<CSSPointValue> Positions
        {
            get { return _positions; }
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

            var values = value as CSSValueList ?? new CSSValueList(value);
            var positions = new List<CSSPointValue>();
            var list = values.ToList();

            foreach (var entry in list)
            {
                if (entry.Length == 0 || entry.Length > 4)
                    return false;

                CSSPointValue position = null;

                if (entry.Length == 1)
                    position = Check(entry[0]);
                else if (entry.Length == 2)
                    position = Check(entry[0], entry[1]);
                else if (entry.Length > 2)
                    position = Check(entry);

                if (position == null)
                    return false;

                positions.Add(position);
            }

            _positions = positions;
            return true;
        }

        static CSSPointValue Check(CSSValueList values)
        {
            var index = 0;
            var shift = CSSCalcValue.Zero;
            var horizontal = CSSCalcValue.Center;
            var vertical = CSSCalcValue.Center;
            var value = values[index];

            if (value.Is("left"))
            {
                horizontal = CSSCalcValue.Zero;
                shift = values[index + 1].AsCalc();
            }
            else if (value.Is("right"))
            {
                horizontal = CSSCalcValue.Full;
                shift = values[index + 1].AsCalc();
            }
            else if (!value.Is("center"))
                return null;

            if (shift != null && shift != CSSCalcValue.Zero)
            {
                index++;
                horizontal = horizontal.Add(shift);
                shift = CSSCalcValue.Zero;
            }

            value = values[++index];

            if (value.Is("top"))
            {
                vertical = CSSCalcValue.Zero;

                if (index + 1 < values.Length)
                    shift = values[index + 1].AsCalc();
            }
            else if (value.Is("bottom"))
            {
                vertical = CSSCalcValue.Full;

                if (index + 1 < values.Length)
                    shift = values[index + 1].AsCalc();
            }
            else if (!value.Is("center"))
                return null;

            if (shift == null)
                return null;
            else if (shift != CSSCalcValue.Zero)
                vertical = vertical.Add(shift);

            return new CSSPointValue(horizontal, vertical);
        }

        static CSSPointValue Check(CSSValue left, CSSValue right)
        {
            var horizontal = left.AsCalc();
            var vertical = right.AsCalc();

            if (horizontal == null)
            {
                if (left.Is("left"))
                    horizontal = CSSCalcValue.Zero;
                else if (left.Is("right"))
                    horizontal = CSSCalcValue.Full;
                else if (left.Is("center"))
                    horizontal = CSSCalcValue.Center;
                else if (left.Is("top"))
                {
                    horizontal = vertical;
                    vertical = CSSCalcValue.Zero;
                }
                else if (left.Is("bottom"))
                {
                    horizontal = vertical;
                    vertical = CSSCalcValue.Full;
                }
            }

            if (vertical == null)
            {
                if (right.Is("top"))
                    vertical = CSSCalcValue.Zero;
                else if (right.Is("bottom"))
                    vertical = CSSCalcValue.Full;
                else if (right.Is("center"))
                    vertical = CSSCalcValue.Center;
                else if (right.Is("left"))
                {
                    vertical = horizontal;
                    horizontal = CSSCalcValue.Zero;
                }
                else if (right.Is("right"))
                {
                    vertical = horizontal;
                    horizontal = CSSCalcValue.Full;
                }
            }

            if (horizontal == null || vertical == null)
                return null;

            return new CSSPointValue(horizontal, vertical);
        }

        static CSSPointValue Check(CSSValue value)
        {
            var calc = value.AsCalc();

            if (calc != null)
                return new CSSPointValue(calc);
            else if (value.Is("left"))
                return new CSSPointValue(x: CSSCalcValue.Zero);
            else if (value.Is("right"))
                return new CSSPointValue(x: CSSCalcValue.Full);
            else if (value.Is("top"))
                return new CSSPointValue(y: CSSCalcValue.Zero);
            else if (value.Is("bottom"))
                return new CSSPointValue(y: CSSCalcValue.Full);
            else if (!value.Is("center"))
                return null;

            return CSSPointValue.Centered;
        }

        #endregion
    }
}

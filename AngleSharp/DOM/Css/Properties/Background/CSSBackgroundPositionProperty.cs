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

        List<Position> _positions;

        #endregion

        #region ctor

        internal CSSBackgroundPositionProperty()
            : base(PropertyNames.BackgroundPosition)
        {
            _inherited = false;
            _positions = new List<Position>();
            _positions.Add(new Position());
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            var positions = new List<Position>();
            var list = values.ToList();

            foreach (var entry in list)
            {
                var position = new Position();

                if (entry.Length == 0 || entry.Length > 4)
                    return false;

                if (entry.Length == 1 && !Check(entry[0], ref position))
                    return false;
                else if (entry.Length == 2 && !Check(entry[0], entry[1], ref position))
                    return false;
                else if (entry.Length > 2 && !Check(entry, ref position))
                    return false;

                positions.Add(position);
            }

            _positions = positions;
            return true;
        }

        static Boolean Check(CSSValueList values, ref Position position)
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
                return false;

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
                return false;

            if (shift == null)
                return false;
            else if (shift != CSSCalcValue.Zero)
                vertical = vertical.Add(shift);

            position.X = horizontal;
            position.Y = vertical;
            return true;
        }

        static Boolean Check(CSSValue left, CSSValue right, ref Position position)
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
                return false;

            position.X = horizontal;
            position.Y = vertical;
            return true;
        }

        static Boolean Check(CSSValue value, ref Position position)
        {
            var calc = value.AsCalc();

            if (calc != null)
                position.X = calc;
            else if (value.Is("left"))
                position.X = CSSCalcValue.Zero;
            else if (value.Is("right"))
                position.X = CSSCalcValue.Full;
            else if (value.Is("top"))
                position.Y = CSSCalcValue.Zero;
            else if (value.Is("bottom"))
                position.Y = CSSCalcValue.Full;
            else if (!value.Is("center"))
                return false;

            return true;
        }

        #endregion

        #region Position

        struct Position
        {
            CSSCalcValue _x;
            CSSCalcValue _y;

            public CSSCalcValue X
            {
                get { return _x ?? CSSCalcValue.Center; }
                set { _x = value; }
            }

            public CSSCalcValue Y
            {
                get { return _y ?? CSSCalcValue.Center; }
                set { _y = value; }
            }
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-position
    /// </summary>
    sealed class CSSBackgroundPositionProperty : CSSProperty
    {
        #region Fields

        List<Position> _positions;

        #endregion

        #region ctor

        public CSSBackgroundPositionProperty()
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
            var temp = new List<CSSValue>();

            for (var i = 0; i < values.Length; i++)
            {
                var position = new Position();

                while (i < values.Length && values[i] != CSSValue.Separator)
                    temp.Add(values[i]);

                if (temp.Count == 0 || temp.Count > 4)
                    return false;

                if (temp.Count == 1 && !Check(temp[0], ref position))
                    return false;
                else if (temp.Count == 2 && !Check(temp[0], temp[1], ref position))
                    return false;
                else if (temp.Count > 2 && !Check(temp, ref position))
                    return false;

                positions.Add(position);
                temp.Clear();
            }

            _positions = positions;
            return true;
        }

        static Boolean Check(List<CSSValue> values, ref Position position)
        {
            throw new NotImplementedException();
        }

        static Boolean Check(CSSValue left, CSSValue right, ref Position position)
        {
            throw new NotImplementedException();
        }

        static Boolean Check(CSSValue value, ref Position position)
        {
            var calc = value.ToCalc();

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

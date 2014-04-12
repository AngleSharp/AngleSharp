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
            //TODO
            return base.IsValid(value);
        }

        #endregion

        #region Position

        struct Position
        {
            CSSCalcValue _x;
            CSSCalcValue _y;

            public CSSCalcValue X
            {
                get { return _x ?? CSSCalcValue.Zero; }
                set { _x = value; }
            }

            public CSSCalcValue Y
            {
                get { return _y ?? CSSCalcValue.Zero; }
                set { _y = value; }
            }
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    sealed class CSSMarginProperty : CSSProperty, ICssMarginProperty
    {
        #region Fields

        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;

        #endregion

        #region ctor

        internal CSSMarginProperty()
            : base(PropertyNames.Margin, PropertyFlags.Shorthand)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the top margin.
        /// </summary>
        public IDistance Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the right margin.
        /// </summary>
        public IDistance Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the bottom margin.
        /// </summary>
        public IDistance Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the left margin.
        /// </summary>
        public IDistance Left
        {
            get { return _left; }
        }

        /// <summary>
        /// Gets if the top margin is automatic.
        /// </summary>
        public Boolean IsTopAuto
        {
            get { return _top == null; }
        }

        /// <summary>
        /// Gets if the right margin is automatic.
        /// </summary>
        public Boolean IsRightAuto
        {
            get { return _right == null; }
        }

        /// <summary>
        /// Gets if the bottom margin is automatic.
        /// </summary>
        public Boolean IsBottomAuto
        {
            get { return _bottom == null; }
        }

        /// <summary>
        /// Gets if the left margin is automatic.
        /// </summary>
        public Boolean IsLeftAuto
        {
            get { return _left == null; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _left = Percent.Zero;
            _right = Percent.Zero;
            _top = Percent.Zero;
            _bottom = Percent.Zero;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSValueList)
                return Check((CSSValueList)value);

            return Check(new CSSValue[1] { value });
        }

        Boolean Check(IEnumerable<CSSValue> values)
        {
            IDistance top = null;
            IDistance right = null;
            IDistance bottom = null;
            IDistance left = null;
            var i = 0;

            foreach (var value in values)
            {
                var distance = value.ToDistance();

                if (distance == null && !value.Is(Keywords.Auto))
                    return false;

                if (i == 0)
                    top = distance;
                else if (i == 1)
                    right = distance;
                else if (i == 2)
                    bottom = distance;
                else if (i == 3)
                    left = distance;
                else
                    return false;

                i++;
            }

            _top = top;
            _right = i > 1 ? right : _top;
            _bottom = i > 2 ? bottom : _top;
            _left = i > 3 ? left : _right;
            return true;
        }

        #endregion
    }
}

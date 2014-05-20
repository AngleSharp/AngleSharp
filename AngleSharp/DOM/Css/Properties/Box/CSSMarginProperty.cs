namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    public sealed class CSSMarginProperty : CSSProperty
    {
        #region Fields

        CSSMarginTopProperty _top;
        CSSMarginRightProperty _right;
        CSSMarginBottomProperty _bottom;
        CSSMarginLeftProperty _left;

        #endregion

        #region ctor

        internal CSSMarginProperty()
            : base(PropertyNames.Margin)
        {
            _inherited = false;
            _left = new CSSMarginLeftProperty();
            _right = new CSSMarginRightProperty();
            _top = new CSSMarginTopProperty();
            _bottom = new CSSMarginBottomProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the top margin.
        /// </summary>
        public CSSCalcValue Top
        {
            get { return _top.Margin; }
        }

        /// <summary>
        /// Gets the value for the right margin.
        /// </summary>
        public CSSCalcValue Right
        {
            get { return _right.Margin; }
        }

        /// <summary>
        /// Gets the value for the bottom margin.
        /// </summary>
        public CSSCalcValue Bottom
        {
            get { return _bottom.Margin; }
        }

        /// <summary>
        /// Gets the value for the left margin.
        /// </summary>
        public CSSCalcValue Left
        {
            get { return _left.Margin; }
        }

        /// <summary>
        /// Gets if the top margin is automatic.
        /// </summary>
        public Boolean IsTopAuto
        {
            get { return _top.IsAuto; }
        }

        /// <summary>
        /// Gets if the right margin is automatic.
        /// </summary>
        public Boolean IsRightAuto
        {
            get { return _right.IsAuto; }
        }

        /// <summary>
        /// Gets if the bottom margin is automatic.
        /// </summary>
        public Boolean IsBottomAuto
        {
            get { return _bottom.IsAuto; }
        }

        /// <summary>
        /// Gets if the left margin is automatic.
        /// </summary>
        public Boolean IsLeftAuto
        {
            get { return _left.IsAuto; }
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
                return Check((CSSValueList)value);

            return Check(new CSSValue[] { value, value, value, value });
        }

        Boolean Check(CSSValueList arguments)
        {
            var count = arguments.Length;

            if (count > 4)
                return false;

            var values = new CSSValue[4];

            for (int i = 0; i < count; i++)
                for (int j = i; j < 4; j += i + 1)
                    values[j] = arguments[i]; 

            return Check(values);
        }

        Boolean Check(CSSValue[] values)
        {
            var target = new CSSProperty[] { new CSSMarginTopProperty(), new CSSMarginRightProperty(), new CSSMarginBottomProperty(), new CSSMarginLeftProperty() };

            for (int i = 0; i < 4; i++)
            {
                target[i].Value = values[i];

                if (target[i].Value != values[i])
                    return false;
            }

            _top = (CSSMarginTopProperty)target[0];
            _right = (CSSMarginRightProperty)target[1];
            _bottom = (CSSMarginBottomProperty)target[2];
            _left = (CSSMarginLeftProperty)target[3];
            return true;
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin
    /// </summary>
    sealed class CSSMarginProperty : CSSProperty
    {
        #region Fields

        CSSMarginTopProperty _top;
        CSSMarginRightProperty _right;
        CSSMarginBottomProperty _bottom;
        CSSMarginLeftProperty _left;

        #endregion

        #region ctor

        public CSSMarginProperty()
            : base(PropertyNames.Margin)
        {
            _inherited = false;
            _left = new CSSMarginLeftProperty();
            _right = new CSSMarginRightProperty();
            _top = new CSSMarginTopProperty();
            _bottom = new CSSMarginBottomProperty();
        }

        #endregion

        #region Methods

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

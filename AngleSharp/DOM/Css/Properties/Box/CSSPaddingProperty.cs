namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CSSPaddingProperty : CSSProperty
    {
        #region Fields

        CSSPaddingTopProperty _top;
        CSSPaddingRightProperty _right;
        CSSPaddingBottomProperty _bottom;
        CSSPaddingLeftProperty _left;

        #endregion

        #region ctor

        public CSSPaddingProperty()
            : base(PropertyNames.Padding)
        {
            _inherited = false;
            _left = new CSSPaddingLeftProperty();
            _right = new CSSPaddingRightProperty();
            _top = new CSSPaddingTopProperty();
            _bottom = new CSSPaddingBottomProperty();
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
            var target = new CSSProperty[] { new CSSPaddingTopProperty(), new CSSPaddingRightProperty(), new CSSPaddingBottomProperty(), new CSSPaddingLeftProperty() };

            for (int i = 0; i < 4; i++)
            {
                target[i].Value = values[i];

                if (target[i].Value != values[i])
                    return false;
            }

            _top = (CSSPaddingTopProperty)target[0];
            _right = (CSSPaddingRightProperty)target[1];
            _bottom = (CSSPaddingBottomProperty)target[2];
            _left = (CSSPaddingLeftProperty)target[3];
            return true;
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    sealed class CSSBorderRadiusProperty : CSSProperty
    {
        #region Fields

        CSSBorderRadiusBottomLeftProperty _bottomLeft;
        CSSBorderRadiusBottomRightProperty _bottomRight;
        CSSBorderRadiusTopLeftProperty _topLeft;
        CSSBorderRadiusTopRightProperty _topRight;

        #endregion

        #region ctor

        public CSSBorderRadiusProperty()
            : base(PropertyNames.BorderRadius)
        {
            _inherited = false;
            _topRight = new CSSBorderRadiusTopRightProperty();
            _bottomRight = new CSSBorderRadiusBottomRightProperty();
            _bottomLeft = new CSSBorderRadiusBottomLeftProperty();
            _topLeft = new CSSBorderRadiusTopLeftProperty();
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
            var splitIndex = arguments.Length;

            for (var i = 0; i < splitIndex; i++)
                if (arguments[i] == CSSValue.Delimiter)
                    splitIndex = i;

            if (count > splitIndex + 4 || splitIndex > 4 || splitIndex == count - 1 || splitIndex == 0)
                return false;

            var values = new CSSValue[4];

            for (int i = 0; i < splitIndex; i++)
                for (int j = i; j < 4; j += i + 1)
                    values[j] = arguments[i];

            if (splitIndex != count)
            {
                var opt = new CSSValue[4];
                splitIndex++;
                count -= splitIndex;

                for (int i = 0; i < count; i++)
                    for (int j = i; j < 4; j += i + 1)
                        opt[j] = arguments[i + splitIndex];

                for (int i = 0; i < 4; i++)
                {
                    var list = new CSSValueList(values[i]);
                    list.Add(opt[i]);
                    values[i] = list;
                }
            }

            return Check(values);
        }

        Boolean Check(CSSValue[] values)
        {
            var target = new CSSProperty[] { new CSSBorderRadiusBottomLeftProperty(), new CSSBorderRadiusBottomRightProperty(), new CSSBorderRadiusTopLeftProperty(), new CSSBorderRadiusTopRightProperty() };

            for (int i = 0; i < 4; i++)
            {
                target[i].Value = values[i];

                if (target[i].Value != values[i])
                    return false;
            }

            _bottomLeft = (CSSBorderRadiusBottomLeftProperty)target[0];
            _bottomRight = (CSSBorderRadiusBottomRightProperty)target[1];
            _topLeft = (CSSBorderRadiusTopLeftProperty)target[2];
            _topRight = (CSSBorderRadiusTopRightProperty)target[3];
            return true;
        }

        #endregion
    }
}

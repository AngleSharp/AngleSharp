namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CSSPaddingProperty : CSSProperty, ICssPaddingProperty
    {
        #region Fields

        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;

        #endregion

        #region ctor

        internal CSSPaddingProperty()
            : base(PropertyNames.Padding)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value for the top padding.
        /// </summary>
        public IDistance Top
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the value for the right padding.
        /// </summary>
        public IDistance Right
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the value for the bottom padding.
        /// </summary>
        public IDistance Bottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the value for the left padding.
        /// </summary>
        public IDistance Left
        {
            get { return _left; }
        }

        #endregion

        #region Methods

        protected override void Reset()
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
            var target = new IDistance[4];

            for (int i = 0; i < 4; i++)
            {
                var distance = values[i].ToDistance();

                if (distance != null)
                    target[i] = distance;
                else if (values[i].Is(Keywords.Auto))
                    target[i] = null;
                else
                    return false;
            }

            _top = target[0];
            _right = target[1];
            _bottom = target[2];
            _left = target[3];
            return true;
        }

        #endregion
    }
}

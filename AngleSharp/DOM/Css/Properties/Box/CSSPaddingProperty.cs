namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/padding
    /// </summary>
    sealed class CSSPaddingProperty : CSSShorthandProperty, ICssPaddingProperty
    {
        #region Fields

        IDistance _top;
        IDistance _right;
        IDistance _bottom;
        IDistance _left;

        #endregion

        #region ctor

        internal CSSPaddingProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Padding, rule)
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

            foreach (var value in values)
            {
                var distance = value.ToDistance();

                if (distance == null)
                    return false;

                if (top == null)
                    top = distance;
                else if (right == null)
                    right = distance;
                else if (bottom == null)
                    bottom = distance;
                else if (left == null)
                    left = distance;
                else
                    return false;
            }

            _top = top;
            _right = right ?? _top;
            _bottom = bottom ?? _top;
            _left = left ?? _right;
            return true;
        }

        #endregion
    }
}

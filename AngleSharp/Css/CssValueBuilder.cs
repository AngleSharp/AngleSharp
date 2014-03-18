namespace AngleSharp.Css
{
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;

    sealed class CssValueBuilder
    {
        #region Fields

        Boolean fraction;
        Stack<FunctionBuffer> function;
        List<CSSValue> values;

        #endregion

        #region ctor

        public CssValueBuilder()
        {
            function = new Stack<FunctionBuffer>();
            values = new List<CSSValue>();
            Reset();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the new value to the current value (or replaces it).
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The status.</returns>
        public void AddValue(CSSValue value)
        {
            if (fraction)
            {
                if (values.Count != 0)
                {
                    var old = values[values.Count - 1];
                    value = new CSSPrimitiveValue(CssUnit.Unknown, old.ToCss() + "/" + value.ToCss());
                    values.RemoveAt(values.Count - 1);
                }

                fraction = false;
            }

            if (function.Count > 0)
                function.Peek().Arguments.Add(value);
            else
                values.Add(value);
        }

        public void Reset()
        {
            fraction = false;
            function.Clear();
            values.Clear();
        }

        public CSSValue ToValue()
        {
            return null;
        }

        #endregion

        #region Function Buffer

        /// <summary>
        /// A buffer for functions.
        /// </summary>
        sealed class FunctionBuffer
        {
            #region Members

            String _name;
            List<CSSValue> _arguments;

            #endregion

            #region ctor

            internal FunctionBuffer(String name)
            {
                this._arguments = new List<CSSValue>();
                this._name = name;
            }

            #endregion

            #region Properties

            public List<CSSValue> Arguments
            {
                get { return _arguments; }
            }

            #endregion

            #region Methods

            public CSSValue ToValue()
            {
                return CSSFunction.Create(_name, _arguments);
            }

            #endregion
        }

        #endregion
    }
}

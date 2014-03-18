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

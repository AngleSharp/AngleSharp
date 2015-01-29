namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    sealed class CssFunction : ICssValue
    {
        #region Fields

        readonly String _name;
        readonly List<ICssValue> _arguments;

        #endregion

        #region ctor

        public CssFunction(String name, List<ICssValue> arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
        }

        public List<ICssValue> Arguments
        {
            get { return _arguments; }
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get
            {
                var arguments = new String[_arguments.Count];

                for (int i = 0; i < arguments.Length; i++)
                    arguments[i] = _arguments[i].CssText;

                return FunctionNames.Build(Name, arguments);
            }
        }

        #endregion
    }
}

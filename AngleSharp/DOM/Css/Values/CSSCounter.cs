namespace AngleSharp.DOM.Css
{
    using System;

    sealed class CSSCounter : CSSValue
    {
        #region Fields

        readonly String _identifier;
        readonly String _listStyle;
        readonly String _separator;

        #endregion

        #region ctor

        public CSSCounter(String identifier, String listStyle, String separator)
        {
            _identifier = identifier;
            _listStyle = listStyle;
            _separator = separator;
        }

        #endregion

        #region Properties

        public String CounterIdentifier
        {
            get { return _identifier; }
        }

        public String ListStyle
        {
            get { return _listStyle; }
        }

        public String DefinedSeparator
        {
            get { return _separator; }
        }

        #endregion

        #region String Representation

        public override String ToCss()
        {
            if (_separator != null)
            {
                if (_listStyle != null)
                    return FunctionNames.Build(FunctionNames.Counters, _identifier, _separator, _listStyle);

                return FunctionNames.Build(FunctionNames.Counters, _identifier, _separator);
            }
            else if (_listStyle != null)
                return FunctionNames.Build(FunctionNames.Counter, _identifier, _listStyle);

            return FunctionNames.Build(FunctionNames.Counter, _identifier);
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css
{
    using System;

    sealed class CSSCounter : CSSPrimitiveValue
    {
        #region Fields

        String _identifier;
        String _listStyle;
        String _separator;

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
    }
}

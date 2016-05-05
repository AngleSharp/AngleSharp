namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The base class token for the CSS parser.
    /// </summary>
    class CssToken
    {
        #region Fields

        readonly CssTokenType _type;
        readonly String _data;
        readonly TextPosition _position;

        public static readonly CssToken Whitespace = new CssToken(CssTokenType.Whitespace, " ", TextPosition.Empty);
        public static readonly CssToken Comma = new CssToken(CssTokenType.Comma, ",", TextPosition.Empty);

        #endregion

        #region ctor

        public CssToken(CssTokenType type, String data, TextPosition position)
        {
            _type = type;
            _data = data;
            _position = position;
        }

        #endregion

        #region Properties

        public TextPosition Position
        {
            get { return _position; }
        }

        public CssTokenType Type
        {
            get { return _type; }
        }

        public String Data
        {
            get { return _data; }
        }

        #endregion

        #region Methods

        public virtual String ToValue()
        {
            return _data;
        }

        #endregion
    }
}

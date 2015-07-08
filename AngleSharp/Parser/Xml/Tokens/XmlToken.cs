namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// The abstract base class of any XML token.
    /// </summary>
    abstract class XmlToken
    {
        #region Fields
        
        readonly XmlTokenType _type;
        readonly TextPosition _position;
        readonly Boolean _ignorable;

        #endregion

        #region ctor

        public XmlToken(XmlTokenType type, TextPosition position, Boolean ignorable = false)
        {
            _type = type;
            _position = position;
            _ignorable = ignorable;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public XmlTokenType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets if the token is a character token and contains a
        /// white-space character.
        /// </summary>
        public Boolean IsIgnorable
        {
            get { return _ignorable; }
        }

        /// <summary>
        /// Gets the position of the token.
        /// </summary>
        public TextPosition Position
        {
            get { return _position; }
        }

        #endregion
    }
}

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

        #endregion

        #region ctor

        public XmlToken(XmlTokenType type, TextPosition position)
        {
            _type = type;
            _position = position;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the token can be ignored.
        /// </summary>
        public virtual Boolean IsIgnorable
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public XmlTokenType Type
        {
            get { return _type; }
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

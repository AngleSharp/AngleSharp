namespace AngleSharp.Parser.Xml
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// The character token that contains a single character.
    /// </summary>
    sealed class XmlCharacterToken : XmlToken
    {
        #region Fields

        readonly Char _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public XmlCharacterToken(TextPosition position)
            : this(position, Symbols.Null)
        {
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        public XmlCharacterToken(TextPosition position, Char data)
            : base(XmlTokenType.Character, position, data.IsSpaceCharacter())
        {
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public Char Data
        {
            get { return _data; }
        }

        #endregion
    }
}

namespace AngleSharp.Parser.Xml
{
    using System;
    using AngleSharp.Extensions;

    /// <summary>
    /// The character token that contains a single character.
    /// </summary>
    sealed class XmlCharacterToken : XmlToken
    {
        #region Fields

        Char _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public XmlCharacterToken()
        {
            _data = Symbols.Null;
            _type = XmlTokenType.Character;
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        /// <param name="data">The character.</param>
        public XmlCharacterToken(Char data)
        {
            _data = data;
            _type = XmlTokenType.Character;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public Char Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets if the character data is actually a space character.
        /// </summary>
        /// <returns>True if the character data is a space character.</returns>
        public override Boolean IsIgnorable
        {
            get { return _data.IsSpaceCharacter(); }
        }

        #endregion
    }
}

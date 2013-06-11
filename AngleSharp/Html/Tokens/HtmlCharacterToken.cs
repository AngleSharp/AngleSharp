using System;

namespace AngleSharp.Html
{
    /// <summary>
    /// The character token that contains a series of characters.
    /// </summary>
    sealed class HtmlCharacterToken : HtmlToken
    {
        #region Members

        char _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public HtmlCharacterToken()
        {
            _data = Specification.NULL;
            _type = HtmlTokenType.Character;
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        /// <param name="data">The character.</param>
        public HtmlCharacterToken(char data)
        {
            _data = data;
            _type = HtmlTokenType.Character;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public char Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets if the character data is NULL.
        /// </summary>
        /// <returns>True if the character token is NULL, otherwise false.</returns>
        public override bool IsNullChar
        {
            get { return _data == Specification.NULL; }
        }

        /// <summary>
        /// Gets if the character data is actually a space character.
        /// </summary>
        /// <returns>True if the character data is a space character.</returns>
        public override bool IsIgnoreable
        {
            get { return Specification.IsSpaceCharacter(_data); }
        }

        #endregion
    }
}

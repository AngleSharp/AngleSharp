namespace AngleSharp.Parser.Html
{
    using System;

    /// <summary>
    /// The character token that contains a single character.
    /// </summary>
    sealed class HtmlCharacterToken : HtmlToken
    {
        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public HtmlCharacterToken()
        {
            _name = String.Empty;
            _type = HtmlTokenType.Character;
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        /// <param name="data">The character.</param>
        public HtmlCharacterToken(Char data)
        {
            _name = data.ToString();
            _type = HtmlTokenType.Character;
        }

        /// <summary>
        /// Creates a new character token with the given characters.
        /// </summary>
        /// <param name="data">The characters.</param>
        public HtmlCharacterToken(String data)
        {
            _name = data;
            _type = HtmlTokenType.Character;
        }

        #endregion
    }
}

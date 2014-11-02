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

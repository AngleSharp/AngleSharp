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

        readonly String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public XmlCharacterToken(TextPosition position)
            : this(position, String.Empty)
        {
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        public XmlCharacterToken(TextPosition position, String data)
            : base(XmlTokenType.Character, position)
        {
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the token only contains spaces.
        /// </summary>
        public override Boolean IsIgnorable
        {
            get { return _data.StripLeadingTrailingSpaces().Length == 0; }
        }

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public String Data
        {
            get { return _data; }
        }

        #endregion
    }
}

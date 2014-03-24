namespace AngleSharp.Parser.Html
{
    using System;

    /// <summary>
    /// The character token that contains a single character.
    /// </summary>
    sealed class HtmlCharacterToken : HtmlToken
    {
        #region Members

        String _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new character token.
        /// </summary>
        public HtmlCharacterToken()
        {
            _data = String.Empty;
            _type = HtmlTokenType.Character;
        }

        /// <summary>
        /// Creates a new character token with the given character.
        /// </summary>
        /// <param name="data">The character.</param>
        public HtmlCharacterToken(Char data)
        {
            _data = data.ToString();
            _type = HtmlTokenType.Character;
        }

        /// <summary>
        /// Creates a new character token with the given characters.
        /// </summary>
        /// <param name="data">The characters.</param>
        public HtmlCharacterToken(String data)
        {
            _data = data;
            _type = HtmlTokenType.Character;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the token can be used with IsHtmlTIP properties.
        /// </summary>
        public override Boolean IsHtmlCompatible
        {
            get { return true; }
        }

        /// <summary>
        /// Gets if the token can be used with IsMathMLTIP properties.
        /// </summary>
        public override Boolean IsMathCompatible
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the data of the character token.
        /// </summary>
        public String Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets if the character data is empty (null or length equal to zero).
        /// </summary>
        /// <returns>True if the character data is actually NULL or empty.</returns>
        public Boolean IsEmpty
        {
            get { return String.IsNullOrEmpty(_data); }
        }

        /// <summary>
        /// Gets if the character data contains actually a non-space character.
        /// </summary>
        /// <returns>True if the character data contains space character.</returns>
        public Boolean HasContent
        {
            get 
            {
                for (int i = 0; i < _data.Length; i++)
                {
                    if (!_data[i].IsSpaceCharacter())
                        return true;
                }

                return false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes all ignorable characters from the beginning.
        /// </summary>
        /// <returns>The trimmed characters.</returns>
        public String TrimStart()
        {
            var i = 0;

            for (i = 0; i < _data.Length; i++)
            {
                if (!_data[i].IsSpaceCharacter())
                    break;
            }

            var t = _data.Substring(0, i);
            _data = _data.Substring(i);
            return t;
        }

        /// <summary>
        /// Removes the a new line in the beginning, if any.
        /// </summary>
        public void RemoveNewLine()
        {
            if (!String.IsNullOrEmpty(_data) && _data[0] == Specification.LF)
                _data = _data.Substring(1);
        }

        #endregion
    }
}

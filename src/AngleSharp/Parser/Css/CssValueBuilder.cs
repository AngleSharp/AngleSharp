namespace AngleSharp.Parser.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The class that is responsible for book-keeping information
    /// about the current CSS value that is been build.
    /// </summary>
    sealed class CssValueBuilder
    {
        #region Fields
        
        readonly List<CssToken> _values;
        CssToken _buffer;
        Boolean _valid;
        Boolean _important;
        Int32 _open;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value builder instance.
        /// </summary>
        public CssValueBuilder()
        {
            _values = new List<CssToken>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the value is finished at the moment.
        /// </summary>
        public Boolean IsReady
        {
            get { return _open == 0 && _values.Count > 0; }
        }

        /// <summary>
        /// Gets if the value is actually valid.
        /// </summary>
        public Boolean IsValid
        {
            get { return _valid && IsReady; }
        }

        /// <summary>
        /// Gets if the value specified the !important flag.
        /// </summary>
        public Boolean IsImportant
        {
            get { return _important; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the currently available result.
        /// </summary>
        public CssValue GetResult()
        {
            return new CssValue(_values);
        }

        /// <summary>
        /// Applies the token to the currently build value.
        /// </summary>
        /// <param name="token">The current token to apply.</param>
        public void Apply(CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.RoundBracketOpen:
                    _open++;
                    Add(token);
                    break;

                case CssTokenType.Function: // e.g. "rgba(...)"
                    Add(token);
                    break;

                case CssTokenType.Ident: // e.g. "auto"
                    _important = CheckImportant(token);
                    break;

                case CssTokenType.RoundBracketClose:
                    _open--;
                    Add(token);
                    break;

                case CssTokenType.Whitespace: // e.g. " "
                    if (_values.Count > 0 && IsSlash(_values[_values.Count - 1]) == false)
                        _buffer = token;
                    break;

                case CssTokenType.Dimension: // e.g. "3px"
                case CssTokenType.Percentage: // e.g. "5%"
                case CssTokenType.Color:// e.g. "#ABCDEF"
                case CssTokenType.Delim:// e.g. "#"
                case CssTokenType.String:// e.g. "'i am a string'"
                case CssTokenType.Url:// e.g. "url('this is a valid URL')"
                case CssTokenType.Number: // e.g. "173"
                case CssTokenType.Comma: // e.g. ","
                    Add(token);
                    break;

                case CssTokenType.Comment:
                    // Should not be considered.
                    break;

                default: // everything else is unexpected
                    _valid = false;
                    Add(token);
                    break;
            }
        }

        /// <summary>
        /// Resets the builder for reprocessing.
        /// </summary>
        public CssValueBuilder Reset()
        {
            _open = 0;
            _valid = true;
            _buffer = null;
            _important = false;
            _values.Clear();
            return this;
        }

        #endregion

        #region Helpers
        
        Boolean CheckImportant(CssToken token)
        {
            if (_values.Count != 0 && token.Data == Keywords.Important)
            {
                var previous = _values[_values.Count - 1];

                if (IsExclamationMark(previous))
                {
                    do _values.RemoveAt(_values.Count - 1);
                    while (_values.Count > 0 && _values[_values.Count - 1].Type == CssTokenType.Whitespace);

                    return true;
                }
            }
            
            Add(token);
            return _important;
        }

        void Add(CssToken token)
        {
            if (_buffer != null && !IsCommaOrSlash(token))
            {
                _values.Add(_buffer);
            }
            else if (_values.Count != 0 && !IsComma(token) && IsComma(_values[_values.Count - 1]))
            {
                _values.Add(CssToken.Whitespace);
            }

            _buffer = null;

            if (_important)
            {
                _valid = false;
            }
            
            _values.Add(token);
        }

        static Boolean IsCommaOrSlash(CssToken token)
        {
            return IsComma(token) || IsSlash(token);
        }

        static Boolean IsComma(CssToken token)
        {
            return token.Type == CssTokenType.Comma;
        }

        static Boolean IsExclamationMark(CssToken token)
        {
            return token.Type == CssTokenType.Delim && token.Data.Has(Symbols.ExclamationMark);
        }

        static Boolean IsSlash(CssToken token)
        {
            return token.Type == CssTokenType.Delim && token.Data.Has(Symbols.Solidus);
        }

        #endregion
    }
}

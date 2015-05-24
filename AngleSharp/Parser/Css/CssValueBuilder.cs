namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Dom.Css;

    /// <summary>
    /// The class that is responsible for book-keeping information
    /// about the current CSS value that is been build.
    /// </summary>
    [DebuggerStepThrough]
    sealed class CssValueBuilder
    {
        #region Fields
        
        readonly List<CssToken> _values;
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
        /// Gets the currently available result.
        /// </summary>
        public CssValue Result
        {
            get
            {
                if (IsValid == false)
                    return null;

                for (int i = _values.Count - 1; i >= 0; i--)
                {
                    if (_values[i].Type == CssTokenType.Whitespace)
                        _values.RemoveAt(i);
                    else
                        break;
                }

                return new CssValue(_values);
            }
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
                    if (_values.Count != 0)
                        Add(token);
                    break;

                case CssTokenType.Dimension: // e.g. "3px"
                case CssTokenType.Percentage: // e.g. "5%"
                case CssTokenType.Hash:// e.g. "#ABCDEF"
                case CssTokenType.Delim:// e.g. "#"
                case CssTokenType.String:// e.g. "'i am a string'"
                case CssTokenType.Url:// e.g. "url('this is a valid URL')"
                case CssTokenType.Number: // e.g. "173"
                case CssTokenType.Comma: // e.g. ","
                    Add(token);
                    break;

                default: // everything else is unexpected
                    _valid = false;
                    break;
            }
        }

        /// <summary>
        /// Resets the builder for reprocessing.
        /// </summary>
        public void Reset()
        {
            _open = 0;
            _valid = true;
            _important = false;
            _values.Clear();
        }

        #endregion

        #region Helpers
        
        Boolean CheckImportant(CssToken token)
        {
            if (_values.Count != 0 && token.Data == Keywords.Important)
            {
                var previous = _values[_values.Count - 1];

                if (previous.Type == CssTokenType.Delim && previous.Data[0] == Symbols.ExclamationMark)
                {
                    _values.RemoveAt(_values.Count - 1);
                    return true;
                }
            }
            
            Add(token);
            return _important;
        }

        void Add(CssToken token)
        {
            if (_important)
                _valid = false;
            else if (_valid)
                _values.Add(token);
        }

        #endregion
    }
}

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
            get { return true; }
        }

        /// <summary>
        /// Gets the currently available result.
        /// </summary>
        public ICssValue Result
        {
            get { return null; }
        }

        /// <summary>
        /// Gets if the value is actually valid.
        /// </summary>
        public Boolean IsValid
        {
            get { return _valid; }
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
                case CssTokenType.Function: // e.g. "rgba(...)"
                case CssTokenType.Dimension: // e.g. "3px"
                case CssTokenType.Percentage: // e.g. "5%"
                case CssTokenType.Hash:// e.g. "#ABCDEF"
                case CssTokenType.Delim:// e.g. "#"
                case CssTokenType.Ident: // e.g. "auto"
                case CssTokenType.String:// e.g. "'i am a string'"
                case CssTokenType.Url:// e.g. "url('this is a valid URL')"
                case CssTokenType.Number: // e.g. "173"
                case CssTokenType.Comma: // e.g. ","
                case CssTokenType.Whitespace: // e.g. " "
                    _values.Add(token);
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
            _valid = true;
            _important = false;
            _values.Clear();
        }

        #endregion

        #region Helpers
        
        /// <summary>
        /// Checks if the provided token is the important identifier.
        /// </summary>
        /// <param name="token">The current token.</param>
        /// <returns>True if token is an important ident, else false.</returns>
        static Boolean CheckImportant(CssToken token)
        {
            return token.Type == CssTokenType.Ident && token.Data == Keywords.Important;
        }

        #endregion
    }
}

namespace AngleSharp.Parser.Css
{
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    [DebuggerStepThrough]
    static class CssParserExtensions
    {
        /// <summary>
        /// Retrieves a number describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The code of the error.</returns>
        public static Int32 GetCode(this CssParseError code)
        {
            return (Int32)code;
        }

        public static void JumpToEndOfDeclaration(this IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.Semicolon:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        public static void JumpToNextSemicolon(this IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.Semicolon:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        public static void JumpToClosedArguments(this IEnumerator<CssToken> tokens)
        {
            var round = 0;
            var curly = 0;
            var square = 0;

            do
            {
                switch (tokens.Current.Type)
                {
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        if (round <= 0 && curly <= 0 && square <= 0)
                            return;
                        round--;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                }
            }
            while (tokens.MoveNext());
        }

        /// <summary>
        /// State that is called once in the head of an unknown @ rule.
        /// </summary>
        /// <param name="tokens">The stream of tokens.</param>
        public static void SkipUnknownRule(this IEnumerator<CssToken> tokens)
        {
            var curly = 0;
            var round = 0;
            var square = 0;
            var cont = true;

            do
            {
                var token = tokens.Current;

                switch (token.Type)
                {
                    case CssTokenType.Semicolon:
                        cont = curly > 0 || round > 0 || square > 0;
                        break;
                    case CssTokenType.CurlyBracketClose:
                        curly--;
                        cont = curly > 0 || round > 0 || square > 0;
                        break;
                    case CssTokenType.Function:
                    case CssTokenType.RoundBracketOpen:
                        round++;
                        break;
                    case CssTokenType.RoundBracketClose:
                        round--;
                        break;
                    case CssTokenType.SquareBracketClose:
                        square--;
                        break;
                    case CssTokenType.SquareBracketOpen:
                        square++;
                        break;
                    case CssTokenType.CurlyBracketOpen:
                        curly++;
                        break;
                }
            }
            while (cont && tokens.MoveNext());
        }

        /// <summary>
        /// Converts the given unit to a value. Uses number for 0.
        /// </summary>
        /// <param name="token">The token to consider.</param>
        /// <returns>The created value.</returns>
        public static ICssValue ToUnit(this CssUnitToken token)
        {
            if (token.Type == CssTokenType.Percentage)
                return new Percent(token.Value);

            return Factory.Units.Create(token.Value, token.Unit.ToLowerInvariant());
        }
    }
}
